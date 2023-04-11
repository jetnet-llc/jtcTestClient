using System.ComponentModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace jtcTestClient
{
  internal class apiConnection
  {

    public async Task<string> GetFromAPI(string bearerToken, string urn, object? json, string method = "GET")
    {
      HttpClient client = new HttpClient();
      var httpResponseMessage = new HttpResponseMessage();

      if (method.ToUpper().Contains("GET"))
      {
        if (!string.IsNullOrWhiteSpace(bearerToken.Trim()))
        {
          client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        httpResponseMessage = client.GetAsync(urn).Result;
      }
      else
      {
        if (!string.IsNullOrWhiteSpace(bearerToken.Trim()))
        {
          client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        httpResponseMessage = client.PostAsJsonAsync(urn, json).Result;

      }

      string responseStatus = httpResponseMessage.StatusCode.ToString();

      if (responseStatus != System.Net.HttpStatusCode.OK.ToString())
        Console.WriteLine("ERROR : " + httpResponseMessage.StatusCode.ToString().Trim() + " - " + httpResponseMessage.ReasonPhrase!.ToString().Trim());


      return await httpResponseMessage.Content.ReadAsStringAsync();
    }

  }

  #region admin_classes
  internal class ApiUser
  {

    public string? EmailAddress { get; set; }
    public string? Password { get; set; }

  };
  internal class responseAPILogin
  {

    public string? bearerToken { get; set; }

    public string? apiToken { get; set; }
  }
  #endregion

  #region aircraft_classes
  public class AcListOptions
  {
    public AcListOptions()
    {
      airframetype = eAirFrameTypes.None;
      maketype = eMakeTypes.None;
      sernbr = "";
      regnbr = "";
      modelid = 0;
      makename = "";
      isforsale = eYesNoIgnoreFlag.Ignore;
      lifecycle = eLifeCycle.None;
      basestate = null;
      basestatename = null;
      basecountry = "";
      basecode = "";
      actiondate = "";
      companyid = 0;
      contactid = 0;
      yearmfr = 0;
      yeardlv = 0;
      aircraftchanges = false;
      aclist = null;
      modlist = null;
    }
    public eAirFrameTypes airframetype { get; set; }
    public eMakeTypes maketype { get; set; }
    public string sernbr { get; set; }
    public string regnbr { get; set; }
    public int modelid { get; set; }
    public string makename { get; set; }
    public eYesNoIgnoreFlag isforsale { get; set; }
    public eLifeCycle lifecycle { get; set; }
    public List<string>? basestate { get; set; }
    public List<string>? basestatename { get; set; }
    public string? basecountry { get; set; }
    public string? basecode { get; set; }
    public string actiondate { get; set; }
    public int companyid { get; set; }
    public int contactid { get; set; }
    public int yearmfr { get; set; }
    public int yeardlv { get; set; }
    public bool aircraftchanges { get; set; }
    public List<int>? aclist { get; set; }
    public List<int>? modlist { get; set; }
  }
  public class responseAircraftList
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int count { get; set; }
    public string? pageurl { get; set; }
    public List<object>? aircraft { get; set; }
  }
  public class aircraftIdentClass
  {
    public int aircraftid { get; set; }
    public int modelid { get; set; }
    public dynamic? actiondate { get; set; }
    public string? pageurl { get; set; }
    public string? make { get; set; }
    public string? model { get; set; }
    public int yearmfg { get; set; }
    public int yeardlv { get; set; }
    public string? maketype { get; set; }
    public string? weightclass { get; set; }
    public string? categorysize { get; set; }
    public string? sernbr { get; set; }
    public string? regnbr { get; set; }
    public dynamic? regnbrexpires { get; set; }
    public string? prevregnbr { get; set; }
    public dynamic? purchasedate { get; set; }
    public string? baseiata { get; set; }
    public string? baseicao { get; set; }
    public string? baseairport { get; set; }
    public string? basecity { get; set; }
    public string? basestate { get; set; }
    public string? basecountry { get; set; }
    public dynamic? baseairportid { get; set; }
    public string? basefaaid { get; set; }

    public aircraftIdentClass()
    {
      aircraftid = 0;
      modelid = 0;
      actiondate = null;
      pageurl = "";
      make = "";
      model = "";
      yearmfg = 0;
      yeardlv = 0;
      maketype = "";
      weightclass = "";
      categorysize = null;
      sernbr = "";
      regnbr = "";
      prevregnbr = "";

      purchasedate = null;

      regnbrexpires = null;

      baseiata = null;
      baseicao = null;
      baseairport = null;
      basecity = null;
      basestate = null;
      basecountry = null;
      baseairportid = null;
      basefaaid = "";

    }

  }
  public class aircraftAirframeClass
  {
    public dynamic? aftt { get; set; }
    public dynamic? landings { get; set; }
    public dynamic? timesasofdate { get; set; }
    public dynamic? estaftt { get; set; }

    public aircraftAirframeClass()
    {
      aftt = null;
      landings = null;
      timesasofdate = null;
      estaftt = null;
    }

  }
  public class aircraftEngineClass
  {
    public dynamic? onconditiontbo { get; set; }
    public string? maintenanceprogram { get; set; }
    public string? model { get; set; }
    public dynamic? enginenoiserating { get; set; }
    public List<engineClass>? engines { get; set; }

    public aircraftEngineClass()
    {
      onconditiontbo = null;
      maintenanceprogram = "";
      model = "";
      enginenoiserating = null;
      engines = null;
    }

  }
  public class engineClass
  {
    public int seqnbr { get; set; }
    public dynamic? serialnum { get; set; }
    public dynamic? ttsnew { get; set; }
    public dynamic? tbo { get; set; }
    public dynamic? tcsn { get; set; }
    public dynamic? sohhrs { get; set; }
    public dynamic? shihrs { get; set; }
    public dynamic? sohcycles { get; set; }
    public dynamic? shscycles { get; set; }

    public engineClass()
    {
      seqnbr = 0;
      serialnum = null;
      ttsnew = null;
      tbo = null;
      tcsn = null;
      sohhrs = null;
      shihrs = null;
      sohcycles = null;
      shscycles = null;

    }

  }
  public class aircraftAPUClass
  {
    public string? model { get; set; }
    public string? sernbr { get; set; }
    public dynamic? ttsnew { get; set; }
    public dynamic? soh { get; set; }
    public string? maintenanceprogram { get; set; }

    public aircraftAPUClass()
    {
      model = "";
      sernbr = "";
      ttsnew = null;
      soh = null;
      maintenanceprogram = "";
    }

  }
  public class compReferenceClass
  {
    public int companyid { get; set; }
    public string? relationtype { get; set; }
    public dynamic? contactid { get; set; }
    public dynamic? ownerpercent { get; set; }
    public dynamic? fractionexpiresdate { get; set; }
    public string? isoperator { get; set; }
    public string? businesstype { get; set; }

    public compReferenceClass()
    {
      companyid = 0;
      relationtype = "";
      contactid = null;
      ownerpercent = null;
      fractionexpiresdate = null;
      isoperator = "N";
      businesstype = "";
    }

  }
  public class aircraftMaintenanceClass
  {
    public string? maintained { get; set; }
    public string? airframemaintenanceprogram { get; set; }
    public string? airframetrackingprogram { get; set; }
    public List<string>? certifications { get; set; }
    public string? notes { get; set; }
    public string? weightscapacity { get; set; }
    public dynamic? damagestatus { get; set; }
    public dynamic? damagenotes { get; set; }
    public List<maintItemClass>? maintenanceitems { get; set; }
    public aircraftMaintenanceClass()
    {
      maintained = "";
      airframemaintenanceprogram = "";
      airframetrackingprogram = "";
      certifications = null;

      notes = null;
      weightscapacity = null;

      damagestatus = null;
      damagenotes = null;

      maintenanceitems = null;

    }

  }
  public class maintItemClass
  {
    public string? name { get; set; }
    public dynamic? compliedwithdate { get; set; }
    public dynamic? duedate { get; set; }
    public string? notes { get; set; }

    public maintItemClass()
    {
      name = "";
      compliedwithdate = null;
      duedate = null;
      notes = "";
    }

  }
  public class aircraftAvionicsClass
  {
    public string? name { get; set; }
    public string? description { get; set; }

    public aircraftAvionicsClass()
    {
      name = "";
      description = "";
    }

  }
  public class aircraftFeaturesClass
  {
    public string? name { get; set; }
    public string? code { get; set; }
    public string? status { get; set; }

    public aircraftFeaturesClass()
    {
      name = "";
      code = "";
      status = "";
    }

  }
  public class detailItemClass
  {
    public string? name { get; set; }
    public string? description { get; set; }

    public detailItemClass()
    {
      name = "";
      description = "";
    }

  }
  public class aircraftLeaseClass
  {
    public dynamic? leasedate { get; set; }
    public string? description { get; set; }
    public string? status { get; set; }
    public string? type { get; set; }
    public string? term { get; set; }
    public string? note { get; set; }
    public dynamic? expirationdate { get; set; }

    public aircraftLeaseClass()
    {
      leasedate = null;
      description = null;
      status = "";
      type = null;
      term = null;
      note = null;
      expirationdate = null;

    }

  }
  public class aircraftFlightsClass
  {
    public int flightyear { get; set; }
    public int flightmonth { get; set; }
    public int flights { get; set; }
    public int flighthours { get; set; }

    public aircraftFlightsClass()
    {
      flightyear = 0;
      flightmonth = 0;
      flights = 0;
      flighthours = 0;

    }

  }
  internal class aircraftClass
  {
    public aircraftIdentClass? identification { get; set; }
    public dynamic? status { get; set; }
    public aircraftAirframeClass? airframe { get; set; }
    public aircraftEngineClass? engine { get; set; }
    public aircraftAPUClass? apu { get; set; }
    public List<compReferenceClass>? companyrelationships { get; set; }
    public aircraftMaintenanceClass? maintenance { get; set; }
    public List<aircraftAvionicsClass>? avionics { get; set; }
    public List<aircraftFeaturesClass>? features { get; set; }
    public List<detailItemClass>? additionalequipment { get; set; }
    public List<detailItemClass>? interior { get; set; }
    public List<detailItemClass>? exterior { get; set; }
    public List<aircraftLeaseClass>? leases { get; set; }
    public List<aircraftFlightsClass>? flights { get; set; }
    public aircraftClass()
    {

      identification = null;
      status = null;
      airframe = null;

      engine = null;
      apu = null;

      companyrelationships = null;

      maintenance = null;
      avionics = null;
      features = null;
      additionalequipment = null;
      interior = null;
      exterior = null;
      leases = null;
      flights = null;

    }

  }
  internal class responseAircraft
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public aircraftClass? aircraft { get; set; }

  }

  #endregion

  #region company_classes

  public class CompListOptions
  {
    public CompListOptions()
    {
      aircraftid = null;
      name = "";
      country = "";
      city = "";
      state = null;
      statename = null;
      bustype = null;
      airframetype = eAirFrameTypes.None;
      maketype = eMakeTypes.None;
      modelid = null;
      makename = null;
      relationship = null;
      isoperator = eYesNoIgnoreFlag.Ignore;
      actiondate = "";
      companychanges = false;
      website = "";
      complist = null;
    }
    public List<int>? aircraftid { get; set; }
    public string name { get; set; }
    public string country { get; set; }
    public string city { get; set; }
    public List<string>? state { get; set; }
    public List<string>? statename { get; set; }
    public List<string>? bustype { get; set; }
    public eAirFrameTypes airframetype { get; set; }
    public eMakeTypes maketype { get; set; }
    public List<int>? modelid { get; set; }
    public List<string>? makename { get; set; }
    public List<string>? relationship { get; set; }
    public eYesNoIgnoreFlag isoperator { get; set; }
    public string actiondate { get; set; }
    public bool companychanges { get; set; }
    public string website { get; set; }
    public List<int>? complist { get; set; }
  }
  public class companyListClass
  {
    public int companyid { get; set; }
    public string name { get; set; }
    public string altname { get; set; }
    public string alttype { get; set; }
    public string address1 { get; set; }
    public string address2 { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string postcode { get; set; }
    public string country { get; set; }
    public string email { get; set; }
    public string website { get; set; }

    public companyListClass()
    {

      companyid = 0;
      name = "";
      altname = "";
      alttype = "";
      address1 = "";
      address2 = "";
      city = "";
      state = "";
      postcode = "";
      country = "";
      email = "";
      website = "";

    }
  }
  public class responseCompanyList
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int count { get; set; }
    public string? pageurl { get; set; }
    public List<companyListClass>? companies { get; set; }
  }
  public class compRelatedClass
  {
    public int companyid { get; set; }
    public dynamic? contactid { get; set; }
    public string relationship { get; set; }

    public compRelatedClass()
    {
      companyid = 0;
      contactid = null;
      relationship = "";
    }

  }
  public class aircraftReferenceClass
  {
    public int aircraftid { get; set; }
    public string relationtype { get; set; }
    public dynamic? contactid { get; set; }
    public dynamic? ownerpercent { get; set; }
    public dynamic? fractionexpiresdate { get; set; }
    public string isoperator { get; set; }
    public string businesstype { get; set; }

    public aircraftReferenceClass()
    {
      aircraftid = 0;
      relationtype = "";
      contactid = null;
      ownerpercent = null;
      fractionexpiresdate = null;
      isoperator = "N";
      businesstype = "";
    }

  }
  public class phoneNumberClass
  {
    public string type { get; set; }
    public string number { get; set; }

    public phoneNumberClass()
    {
      type = "";
      number = "";

    }

  }
  public class companyIdentClass
  {
    public int companyid { get; set; }
    public dynamic? actiondate { get; set; }
    public string name { get; set; }
    public string altname { get; set; }
    public string alttype { get; set; }
    public string address1 { get; set; }
    public string address2 { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string postcode { get; set; }
    public string country { get; set; }
    public string continent { get; set; }
    public string agencytype { get; set; }
    public string email { get; set; }
    public string website { get; set; }
    public string pageurl { get; set; }

    public companyIdentClass()
    {

      companyid = 0;
      actiondate = null;
      name = "";
      altname = "";
      alttype = "";
      address1 = "";
      address2 = "";
      city = "";
      state = "";
      postcode = "";
      country = "";
      continent = "";
      agencytype = "";
      email = "";
      website = "";
      pageurl = "";

    }
  }
  public class companyClass
  {
    public companyIdentClass? identification { get; set; }
    public List<int>? contacts { get; set; }
    public List<phoneNumberClass>? phonenumbers { get; set; }
    public List<string>? businesstypes { get; set; }
    public List<aircraftReferenceClass>? aircraftrelationships { get; set; }
    public List<compRelatedClass>? relatedcompanies { get; set; }

    public companyClass()
    {

      identification = null;

      contacts = null;
      phonenumbers = null;

      businesstypes = null;
      aircraftrelationships = null;
      relatedcompanies = null;
    }

  }
  public class responseCompany
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public companyClass? company { get; set; }
  }

  #endregion

  #region contact_classes

  public class ContListOptions
  {
    public ContListOptions()
    {
      aircraftid = null;
      companyid = 0;
      companyname = "";
      firstname = "";
      lastname = "";
      title = "";
      email = "";
      actiondate = "";
      phonenumber = "";
      contactchanges = false;
      contlist = null;
    }

    public List<int>? aircraftid { get; set; }
    public int companyid { get; set; }
    public string companyname { get; set; }
    public string firstname { get; set; }
    public string lastname { get; set; }
    public string title { get; set; }
    public string email { get; set; }
    public string actiondate { get; set; }
    public string phonenumber { get; set; }
    public bool contactchanges { get; set; }
    public List<int>? contlist { get; set; }


  }
  public class contactListClass
  {
    public int contactid { get; set; }
    public int companyid { get; set; }
    public dynamic? companyname { get; set; }
    public dynamic? sirname { get; set; }
    public dynamic? firstname { get; set; }
    public dynamic? middleinitial { get; set; }
    public dynamic? lastname { get; set; }
    public dynamic? suffix { get; set; }
    public dynamic? title { get; set; }
    public dynamic? email { get; set; }
    public dynamic? phonenumber { get; set; }
    public dynamic? actiondate { get; set; }

    public contactListClass()
    {

      contactid = 0;
      companyid = 0;
      companyname = null;
      sirname = null;
      firstname = null;
      middleinitial = null;
      lastname = null;
      suffix = null;
      title = null;
      email = null;
      phonenumber = null;
      actiondate = null;

    }
  }
  public class responseContactList
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int count { get; set; }
    public string? pageurl { get; set; }
    public List<contactListClass>? contacts { get; set; }
  }
  public class contactOtherClass
  {
    public int companyid { get; set; }
    public dynamic? contactid { get; set; }

    public contactOtherClass()
    {
      companyid = 0;
      contactid = null;
    }

  }
  public class aircraftContactRefClass
  {
    public int aircraftid { get; set; }
    public string relationtype { get; set; }
    public dynamic? ownerpercent { get; set; }
    public dynamic? fractionexpiresdate { get; set; }
    public string isoperator { get; set; }
    public string businesstype { get; set; }

    public aircraftContactRefClass()
    {
      aircraftid = 0;
      relationtype = "";
      ownerpercent = null;
      fractionexpiresdate = null;
      isoperator = "N";
      businesstype = "";
    }

  }
  public class contactIdentClass
  {
    public int contactid { get; set; }
    public int companyid { get; set; }
    public dynamic? actiondate { get; set; }
    public dynamic? sirname { get; set; }
    public dynamic? firstname { get; set; }
    public dynamic? middleinitial { get; set; }
    public dynamic? lastname { get; set; }
    public dynamic? suffix { get; set; }
    public dynamic? title { get; set; }
    public dynamic? email { get; set; }
    public string pageurl { get; set; }

    public contactIdentClass()
    {

      contactid = 0;
      companyid = 0;
      actiondate = null;
      sirname = null;
      firstname = null;
      middleinitial = null;
      lastname = null;
      suffix = null;
      title = null;
      email = null;
      pageurl = "";

    }
  }
  public class contactClass
  {
    public contactIdentClass? identification { get; set; }
    public List<phoneNumberClass>? phonenumbers { get; set; }
    public List<aircraftContactRefClass>? aircraftrelationships { get; set; }
    public List<contactOtherClass>? otherlistings { get; set; }

    public contactClass()
    {

      identification = null;
      phonenumbers = null;

      aircraftrelationships = null;
      otherlistings = null;

    }
  }
  public class responseContact
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }

    public contactClass? contact { get; set; }
  }

  #endregion

  #region utility_classes

  public enum eAirFrameTypes : int
  {
    [Description("None")]
    [JsonPropertyName("N")]
    None = 0,

    [Description("Fixed Wing")]
    [JsonPropertyName("F")]
    FixedWing = 1,

    [Description("Rotary")]
    [JsonPropertyName("R")]
    Rotary = 2,
  }
  public enum eMakeTypes : int
  {
    [Description("None")]
    [JsonPropertyName("N")]
    None = 0,

    [Description("Jet Airliner")]
    [JsonPropertyName("E")]
    JetAirliner = 1,

    [Description("Business Jet")]
    [JsonPropertyName("J")]
    BusinessJet = 2,

    [Description("Turboprop")]
    [JsonPropertyName("T")]
    Turboprop = 3,

    [Description("Piston")]
    [JsonPropertyName("P")]
    Piston = 4,

    [Description("Turbine")]
    [JsonPropertyName("U")]
    Turbine = 5
  }
  public enum eYesNoIgnoreFlag : int
  {
    [Description("Ignore")]
    [JsonPropertyName("I")]
    Ignore = 0,

    [Description("Yes")]
    [JsonPropertyName("Y")]
    Yes = 1,

    [Description("No")]
    [JsonPropertyName("N")]
    No = 2
  }
  public enum eLifeCycle : int
  {
    [Description("None")]
    [JsonPropertyName("N")]
    None = 0,

    [Description("In Production")]
    [JsonPropertyName("P")]
    InProduction = 1,

    [Description("At Manufacturer")]
    [JsonPropertyName("M")]
    AtManufacturer = 2,

    [Description("In Operation")]
    [JsonPropertyName("O")]
    InOperation = 3,

    [Description("Retired")]
    [JsonPropertyName("R")]
    Retired = 4,

    [Description("In Storage")]
    [JsonPropertyName("S")]
    InStorage = 5
  }
  public class responseAccountInfo
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public string? servicetype { get; set; }
    public string? servicefrequency { get; set; }
    public long maxrecords { get; set; }
    public long subid { get; set; }
    public bool historyavailable { get; set; }
    public bool flightsavailable { get; set; }

  }
  public class modelClass
  {
    public int modelid { get; set; }
    public eAirFrameTypes airframetype { get; set; }
    public eMakeTypes maketype { get; set; }
    public string make { get; set; }
    public string model { get; set; }
    public string manufacturer { get; set; }
    public string weightclass { get; set; }
    public string categorysize { get; set; }

    public modelClass()
    {

      modelid = 0;
      airframetype = eAirFrameTypes.None;
      maketype = eMakeTypes.None;

      make = "";
      model = "";
      manufacturer = "";

      weightclass = "";
      categorysize = "";

    }
  }
  public class responseAcModels
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int count { get; set; }
    public List<modelClass>? modellist { get; set; }

  }

  public class UpperCaseNamingPolicy : JsonNamingPolicy
  {
    public override string ConvertName(string name) => name.ToUpper();
  }

  public class LowerCaseNamingPolicy : JsonNamingPolicy
  {
    public override string ConvertName(string name) => name.ToLower();
  }
  #endregion
}
