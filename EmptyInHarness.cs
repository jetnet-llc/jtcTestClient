using System.Text.Json;

namespace jtcTestClient
{
  /// <summary>
  /// Non-interactive post-deploy verification for the empty-IN() SQL bug class (CUST-API-20260610 /
  /// "Incorrect syntax near ')'", SqlException 102). Drives the endpoints that historically threw it
  /// with inputs that force an empty / out-of-range IN ( ) path:
  ///   - a no-match search        -> resolves to zero ids
  ///   - a page beyond the result -> empty page slice (exercises the "AND 1 = 0" SQL guard)
  ///   - an empty complist        -> input-validation guard
  ///   - non-existent ids         -> a valid (non-empty) IN that returns zero rows
  /// Pre-fix build  -> HTTP 500 / "Incorrect syntax near ')'".
  /// Fixed build    -> responsestatus SUCCESS (empty result set) or a clean validation message.
  ///
  /// Run it via the console app:  jtcTestClient emptyintest [live|test]   (default: live)
  /// </summary>
  internal static class EmptyInHarness
  {
    const string LIVE = "https://customer.jetnetconnect.com/api/";
    const string TEST = "https://testcustomer.jetnetconnect.com/api/";

    // a company name that matches nothing -> zero resolved ids -> empty IN on the unguarded build
    const string NOMATCH = "ZZQX9NOMATCH7788X";

    // a valid (non-empty) id list whose ids do not exist -> IN ( ?, ? ) that simply returns zero rows
    static readonly List<int> BOGUS_IDS = new() { 999999990, 999999991 };

    public static async Task<int> RunAsync(string env)
    {
      string apiBase = string.Equals(env, "test", StringComparison.OrdinalIgnoreCase) ? TEST : LIVE;

      Console.WriteLine("=== jtc empty-IN() verification harness ===");
      Console.WriteLine($"target: {(apiBase == TEST ? "TEST" : "LIVE")}  {apiBase}");
      Console.WriteLine($"time:   {DateTime.UtcNow:yyyy-MM-ddTHH:mm:ssZ} (UTC)\n");

      var api = new apiConnection();
      var login = new ApiUser { EmailAddress = "demo@jetnet.com", Password = "g846ii2v" };

      string loginRaw = await api.GetFromAPI("", apiBase + "Admin/APILogin", login, "PUT");
      responseAPILogin? lr = null;
      try { lr = JsonSerializer.Deserialize<responseAPILogin>(loginRaw); } catch { /* fall through */ }
      if (lr?.apiToken is null || string.IsNullOrWhiteSpace(lr.apiToken))
      {
        Console.WriteLine("LOGIN FAILED: " + Trunc(loginRaw, 400));
        return 2;
      }
      string token = lr.apiToken!.Trim();
      string bearer = (lr.bearerToken ?? "").Trim();
      Console.WriteLine($"login OK (token chars: {token.Length})\n");

      int pass = 0, fail = 0, unknown = 0;

      async Task Run(string label, string urn, object body)
      {
        string r;
        try { r = await api.GetFromAPI(bearer, apiBase + urn, body, "PUT"); }
        catch (Exception ex) { r = "EXCEPTION: " + ex.Message; }

        string status = ExtractStatus(r);
        string verdict;
        if (r.Contains("Incorrect syntax near", StringComparison.OrdinalIgnoreCase)) { verdict = "FAIL"; fail++; }
        else if (status.Contains("SUCCESS", StringComparison.OrdinalIgnoreCase)) { verdict = "PASS"; pass++; }
        else { verdict = "????"; unknown++; }

        Console.WriteLine($"[{verdict}] {label}");
        Console.WriteLine($"        {status}\n");
      }

      // ---- empty-IN() cases (each previously threw on the unguarded build) ----
      await Run("getCompanyContactList       (no-match name, non-paged)", $"Company/getCompanyContactList/{token}",
          new CompContactListOptions { name = NOMATCH, showcontacts = true });

      await Run("getCompanyContactListPaged  (page beyond results -> SQL AND 1=0 guard)", $"Company/getCompanyContactListPaged/{token}/10/99999",
          new CompContactListOptions { name = "ab", showcontacts = true });

      await Run("getAllCompanyObjects        (empty complist -> validation guard)", $"Company/getAllCompanyObjects/{token}/0/0",
          new CompObjectListOptions { complist = new List<int>() });

      await Run("getAllCompanyObjects        (non-existent ids -> non-empty IN, zero rows)", $"Company/getAllCompanyObjects/{token}/0/0",
          new CompObjectListOptions { complist = BOGUS_IDS });

      await Run("getCompanyList              (no-match name)", $"Company/getCompanyList/{token}",
          new CompListOptions { name = NOMATCH });

      await Run("getContactList              (no-match companyname)", $"Contact/getContactList/{token}",
          new ContListOptions { companyname = NOMATCH });

      Console.WriteLine($"=== RESULT: PASS={pass}  FAIL={fail}  UNKNOWN={unknown} ===");
      Console.WriteLine(fail > 0
          ? "FAIL = 'Incorrect syntax near' still thrown -> a site is unguarded."
          : (unknown > 0 ? "No empty-IN errors; UNKNOWN = guarded earlier (e.g. input validation / 404 route)."
                         : "All guarded: every empty-IN path returns SUCCESS."));
      return fail > 0 ? 1 : 0;
    }

    static string ExtractStatus(string body)
    {
      try
      {
        using var doc = JsonDocument.Parse(body);
        if (doc.RootElement.TryGetProperty("responsestatus", out var s)) return s.GetString() ?? "(null responsestatus)";
        return "(no responsestatus) " + Trunc(body, 240);
      }
      catch { return Trunc((body ?? "").Replace("\r", " ").Replace("\n", " "), 240); }
    }

    static string Trunc(string s, int n) => string.IsNullOrEmpty(s) ? "(empty)" : (s.Length <= n ? s : s.Substring(0, n) + "…");
  }
}
