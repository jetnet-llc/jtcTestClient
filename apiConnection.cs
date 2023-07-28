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
      try
      {

        HttpClient client = new HttpClient()
        {
          Timeout = TimeSpan.FromSeconds(300) // set the time out to 300 seconds (5 minutes)
        };

        var httpResponseMessage = new HttpResponseMessage();

        if (method.ToUpper().Contains("GET"))
        {
          if (bearerToken is not null && !string.IsNullOrWhiteSpace(bearerToken.Trim()))
          {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
          }

          httpResponseMessage = client.GetAsync(urn).Result;
        }
        else
        {
          if (bearerToken is not null && !string.IsNullOrWhiteSpace(bearerToken.Trim()))
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
      catch (Exception ex)
      {
        return ex.ToString();
      }

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
      make = "";
      forsale = null; // eYesNoIgnoreFlag.Ignore;
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
      aircraftchanges = null;
      aclist = null;
      modlist = null;
    }

    public eAirFrameTypes airframetype { get; set; }
    public eMakeTypes maketype { get; set; }
    public string sernbr { get; set; }
    public string regnbr { get; set; }
    public int modelid { get; set; }
    public string make { get; set; }
    public string? forsale { get; set; } //eYesNoIgnoreFlag forsale { get; set; }
    public eLifeCycle lifecycle { get; set; }
    public List<string>? basestate { get; set; }
    public List<string>? basestatename { get; set; }
    public string basecountry { get; set; }
    public string basecode { get; set; }
    public string actiondate { get; set; }
    public int companyid { get; set; }
    public int contactid { get; set; }
    public int yearmfr { get; set; }
    public int yeardlv { get; set; }
    public string? aircraftchanges { get; set; }
    public List<int>? aclist { get; set; }
    public List<int>? modlist { get; set; }
  }
  public class responseAircraftList
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int count { get; set; }
    public string? pagelink { get; set; }
    public List<object>? aircraft { get; set; }
  }
  public class aircraftIdentClass
  {
    public int aircraftid { get; set; }
    public int modelid { get; set; }
    public dynamic? actiondate { get; set; }
    public string? pagelink { get; set; }
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
      pagelink = "";
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
  public class AcHistoryOptions
  {
    public AcHistoryOptions()
    {
      aircraftid = 0;
      airframetype = eAirFrameTypes.None;
      maketype = eMakeTypes.None;
      modelid = 0;
      make = "";
      companyid = 0;
      isnewaircraft = eYesNoIgnoreFlag.Ignore;
      allrelationships = true;
      transtype = null;
      startdate = "";
      enddate = "";
      aclist = null;
      modlist = null;
      lastactionstartdate = "";
      lastactionenddate = "";
      ispreownedtrans = eYesNoIgnoreFlag.Ignore;
      isretailtrans = eYesNoIgnoreFlag.Ignore;
      isinternaltrans = eYesNoIgnoreFlag.Ignore;
    }

    public int aircraftid { get; set; }
    public eAirFrameTypes airframetype { get; set; }
    public eMakeTypes maketype { get; set; }
    public int modelid { get; set; }
    public string make { get; set; }
    public int companyid { get; set; }
    public eYesNoIgnoreFlag isnewaircraft { get; set; }
    public bool allrelationships { get; set; }
    public List<eAcTransTypes>? transtype { get; set; }
    public string startdate { get; set; }
    public string enddate { get; set; }
    public List<int>? aclist { get; set; }
    public List<int>? modlist { get; set; }
    public string lastactionstartdate { get; set; }
    public string lastactionenddate { get; set; }
    public eYesNoIgnoreFlag ispreownedtrans { get; set; }
    public eYesNoIgnoreFlag isretailtrans { get; set; }
    public eYesNoIgnoreFlag isinternaltrans { get; set; }
  }
  internal class aircraftHistoryListClass
  {

    [JsonPropertyName("aircraftid")]
    public int ac_id { get; set; }

    [JsonPropertyName("journalid")]
    public int journ_id { get; set; }

    [JsonPropertyName("modelid")]
    public int amod_id { get; set; }

    [JsonPropertyName("actiondate")]
    public dynamic? ac_action_date { get; set; }

    [JsonPropertyName("pagelink")]
    public string ac_page_url { get; set; }

    [JsonPropertyName("make")]
    public string amod_make_name { get; set; }

    [JsonPropertyName("model")]
    public string amod_model_name { get; set; }

    [JsonPropertyName("yearmfr")]
    public int ac_mfr_year { get; set; }

    [JsonPropertyName("yeardlv")]
    public int ac_year { get; set; }

    public eMakeTypes ac_maketype { get; set; }

    [JsonPropertyName("weightclass")]
    public string ac_weightclass { get; set; }

    [JsonPropertyName("categorysize")]
    public dynamic? ac_jniqcategory { get; set; }

    [JsonPropertyName("sernbr")]
    public string ac_ser_no_full { get; set; }

    [JsonPropertyName("regnbr")]
    public string ac_reg_no { get; set; }

    [JsonPropertyName("transtype")]
    public string? jcat_subcategory_name { get; set; }

    [JsonPropertyName("transdate")]
    public dynamic? journ_date { get; set; }

    [JsonPropertyName("translastactiondate")]
    public dynamic? translastactiondate { get; set; }

    [JsonPropertyName("description")]
    public string? journ_subject { get; set; }

    [JsonPropertyName("customernote")]
    public string? journ_customer_note { get; set; }

    [JsonPropertyName("internaltrans")]
    public bool journ_internal_trans_flag { get; set; }

    [JsonPropertyName("newac")]
    public string journ_newac_flag { get; set; }

    [JsonPropertyName("usage")]
    public string ac_usage { get; set; }

    [JsonPropertyName("companyrelationships")]
    public List<compReferenceClass>? ac_ref_companies { get; set; }

    public aircraftHistoryListClass()
    {
      ac_id = 0;
      journ_id = 0;
      amod_id = 0;

      ac_action_date = null;
      ac_page_url = "";

      amod_make_name = "";
      amod_model_name = "";

      ac_mfr_year = 0;
      ac_year = 0;
      ac_maketype = eMakeTypes.None;
      ac_weightclass = "";
      ac_jniqcategory = null;
      ac_ser_no_full = "";
      ac_reg_no = "";

      jcat_subcategory_name = null;
      journ_date = null;
      translastactiondate = null;
      journ_subject = null;
      journ_customer_note = null;

      journ_internal_trans_flag = false;
      journ_newac_flag = "";
      ac_usage = "";
      ac_ref_companies = null;

    }

  }
  internal class responseAcHistory
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int count { get; set; }
    public List<aircraftHistoryListClass>? history { get; set; }
  }

  internal class AcFlightDataOptions
  {
    public AcFlightDataOptions()
    {
      aircraftid = 0;
      airframetype = eAirFrameTypes.None;
      sernbr = "";
      regnbr = "";
      maketype = eMakeTypes.None;
      modelid = 0;
      make = "";
      origin = "";
      destination = "";
      startdate = "";
      enddate = "";
      aclist = null;
      modlist = null;
      lastactionstartdate = "";
      lastactionenddate = "";
    }

    public int aircraftid { get; set; }
    public eAirFrameTypes airframetype { get; set; }
    public string sernbr { get; set; }
    public string regnbr { get; set; }
    public eMakeTypes maketype { get; set; }
    public int modelid { get; set; }
    public string make { get; set; }
    public string origin { get; set; }
    public string destination { get; set; }
    public string startdate { get; set; }
    public string enddate { get; set; }
    public List<int>? aclist { get; set; }
    public List<int>? modlist { get; set; }
    public string lastactionstartdate { get; set; }
    public string lastactionenddate { get; set; }
  }
  internal class responseAcFlightData
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int count { get; set; }
    public List<aircraftFightDataClass>? flightdata { get; set; }
  }

  internal class aircraftFightDataClass
  {
    // aircraft 
    [JsonPropertyName("make")]
    public string make_name { get; set; }

    [JsonPropertyName("model")]
    public string model_name { get; set; }

    public eMakeTypes model_maketype { get; set; }

    [JsonPropertyName("modelmfr")]
    public string model_mfr { get; set; }

    [JsonPropertyName("categorysize")]
    public string model_category { get; set; }

    [JsonPropertyName("modelicao")]
    public string model_icao { get; set; }

    [JsonPropertyName("modelid")]
    public int model_id { get; set; }

    [JsonPropertyName("yearmfr")]
    public int ac_yearmfr { get; set; }

    [JsonPropertyName("sernbr")]
    public string ac_sernbr { get; set; }

    [JsonPropertyName("regnbr")]
    public string ac_regnbr { get; set; }

    [JsonPropertyName("aircraftid")]
    public int ac_id { get; set; }

    // flight  
    [JsonPropertyName("flightdate")]
    public dynamic? ac_flightdate { get; set; }

    [JsonPropertyName("flighttime")]
    public dynamic? ac_flighttime { get; set; }

    [JsonPropertyName("distance")]
    public int ac_distance { get; set; }

    [JsonPropertyName("estfuelburn")]
    public decimal ac_estfuelburn { get; set; }

    [JsonPropertyName("flightid")]
    public string ac_flightid { get; set; }

    // origin   
    [JsonPropertyName("origin_date")]
    public dynamic? ac_origin_date { get; set; }

    [JsonPropertyName("origin_time")]
    public dynamic? ac_origin_time { get; set; }

    [JsonPropertyName("origin_aport_code")]
    public string ac_origin_aport_code { get; set; }

    [JsonPropertyName("origin_name")]
    public string ac_origin_name { get; set; }

    [JsonPropertyName("origin_city")]
    public string ac_origin_city { get; set; }

    [JsonPropertyName("origin_state")]
    public dynamic? ac_origin_state { get; set; }

    [JsonPropertyName("origin_country")]
    public string ac_origin_country { get; set; }

    [JsonPropertyName("origin_iata_code")]
    public string ac_origin_iata_code { get; set; }

    [JsonPropertyName("origin_icao_code")]
    public string ac_origin_icao_code { get; set; }

    [JsonPropertyName("origin_faaid_code")]
    public string ac_origin_faaid_code { get; set; }

    [JsonPropertyName("origin_latitude")]
    public string ac_origin_latitude { get; set; }

    [JsonPropertyName("origin_longitude")]
    public string ac_origin_longitude { get; set; }

    [JsonPropertyName("origin_continent")]
    public string ac_origin_continent { get; set; }

    [JsonPropertyName("origin_jetnet_id")]
    public int ac_origin_id { get; set; }

    // dest 
    [JsonPropertyName("dest_date")]
    public dynamic? ac_dest_date { get; set; }

    [JsonPropertyName("dest_time")]
    public dynamic? ac_dest_time { get; set; }

    [JsonPropertyName("dest_aport_code")]
    public string ac_dest_aport_code { get; set; }

    [JsonPropertyName("dest_name")]
    public string ac_dest_name { get; set; }

    [JsonPropertyName("dest_city")]
    public string ac_dest_city { get; set; }

    [JsonPropertyName("dest_state")]
    public dynamic? ac_dest_state { get; set; }

    [JsonPropertyName("dest_country")]
    public string ac_dest_country { get; set; }

    [JsonPropertyName("dest_iata_code")]
    public string ac_dest_iata_code { get; set; }

    [JsonPropertyName("dest_icao_code")]
    public string ac_dest_icao_code { get; set; }

    [JsonPropertyName("dest_faaid_code")]
    public string ac_dest_faaid_code { get; set; }

    [JsonPropertyName("dest_latitude")]
    public string ac_dest_latitude { get; set; }

    [JsonPropertyName("dest_longitude")]
    public string ac_dest_longitude { get; set; }

    [JsonPropertyName("dest_continent")]
    public string ac_dest_continent { get; set; }

    [JsonPropertyName("dest_jetnet_id")]
    public int ac_dest_id { get; set; }

    // base 
    [JsonPropertyName("base_name")]
    public string ac_base_name { get; set; }

    [JsonPropertyName("base_city")]
    public string ac_base_city { get; set; }

    [JsonPropertyName("base_state")]
    public dynamic? ac_base_state { get; set; }

    [JsonPropertyName("base_country")]
    public string ac_base_country { get; set; }

    [JsonPropertyName("base_iata_code")]
    public string ac_base_iata_code { get; set; }

    [JsonPropertyName("base_icao_code")]
    public string ac_base_icao_code { get; set; }

    [JsonPropertyName("base_faaid_code")]
    public string ac_base_faaid_code { get; set; }

    [JsonPropertyName("base_jetnet_id")]
    public int ac_base_id { get; set; }

    // operator
    [JsonPropertyName("operator_name")]
    public string ac_operator_name { get; set; }

    [JsonPropertyName("operator_address1")]
    public string ac_operator_address1 { get; set; }

    [JsonPropertyName("operator_address2")]
    public string ac_operator_address2 { get; set; }

    [JsonPropertyName("operator_city")]
    public string ac_operator_city { get; set; }

    [JsonPropertyName("operator_state")]
    public dynamic? ac_operator_state { get; set; }

    [JsonPropertyName("operator_postcode")]
    public dynamic? ac_operator_zipcode { get; set; }

    [JsonPropertyName("operator_country")]
    public string ac_operator_country { get; set; }

    [JsonPropertyName("operator_web_address")]
    public dynamic? ac_operator_web_address { get; set; }

    [JsonPropertyName("operator_jetnet_id")]
    public int ac_operator_id { get; set; }

    public aircraftFightDataClass()
    {
      make_name = "";
      model_name = "";
      model_maketype = eMakeTypes.None;
      model_mfr = "";
      model_category = "";
      model_icao = "";
      model_id = 0;
      ac_yearmfr = 0;
      ac_sernbr = "";
      ac_regnbr = "";
      ac_id = 0;

      ac_flightdate = null;
      ac_flighttime = null;
      ac_distance = 0;
      ac_estfuelburn = 0;
      ac_flightid = "";

      ac_origin_date = null;
      ac_origin_time = null;
      ac_origin_aport_code = "";
      ac_origin_name = "";
      ac_origin_city = "";
      ac_origin_state = null;
      ac_origin_country = "";
      ac_origin_iata_code = "";
      ac_origin_icao_code = "";
      ac_origin_faaid_code = "";
      ac_origin_latitude = "";
      ac_origin_longitude = "";
      ac_origin_continent = "";
      ac_origin_id = 0;

      ac_dest_date = null;
      ac_dest_time = null;
      ac_dest_aport_code = "";
      ac_dest_name = "";
      ac_dest_city = "";
      ac_dest_state = null;
      ac_dest_country = "";
      ac_dest_iata_code = "";
      ac_dest_icao_code = "";
      ac_dest_faaid_code = "";
      ac_dest_latitude = "";
      ac_dest_longitude = "";
      ac_dest_continent = "";
      ac_dest_id = 0;

      ac_base_name = "";
      ac_base_city = "";
      ac_base_state = null;
      ac_base_country = "";
      ac_base_iata_code = "";
      ac_base_icao_code = "";
      ac_base_faaid_code = "";
      ac_base_id = 0;

      ac_operator_name = "";
      ac_operator_address1 = "";
      ac_operator_address2 = "";
      ac_operator_city = "";
      ac_operator_state = null;
      ac_operator_zipcode = null;
      ac_operator_country = "";
      ac_operator_web_address = null;
      ac_operator_id = 0;
    }
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
      make = null;
      relationship = null;
      isoperator = null; // eYesNoIgnoreFlag.Ignore;
      actiondate = "";
      companychanges = null;
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
    public List<string>? make { get; set; }
    public List<string>? relationship { get; set; }
    public string? isoperator { get; set; }
    public string actiondate { get; set; }
    public string? companychanges { get; set; }
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
    public string? pagelink { get; set; }
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
    public string pagelink { get; set; }

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
      pagelink = "";

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
      contactchanges = null;
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
    public string? contactchanges { get; set; }
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
    public string? pagelink { get; set; }
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
    public string pagelink { get; set; }

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
      pagelink = "";

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
  public enum eAcTransTypes : int
  {
    [Description("None")]
    [JsonPropertyName("N")]
    None = 0,

    [Description("Full Sale")]
    [JsonPropertyName("P")]
    FullSale = 1,

    [Description("Fractional Sale")]
    [JsonPropertyName("M")]
    FractionalSale = 2,

    [Description("Share Sale")]
    [JsonPropertyName("O")]
    ShareSale = 3,

    [Description("Lease")]
    [JsonPropertyName("R")]
    Lease = 4,

    [Description("Delivery Position")]
    [JsonPropertyName("S")]
    DeliveryPosition = 5,

    [Description("Foreclosure")]
    [JsonPropertyName("N")]
    Foreclosure = 6,

    [Description("Seizure")]
    [JsonPropertyName("P")]
    Seizure = 7,

    [Description("Made Available")]
    [JsonPropertyName("M")]
    MadeAvailable = 8,

    [Description("Off Market")]
    [JsonPropertyName("O")]
    OffMarket = 9,

    [Description("Written Off")]
    [JsonPropertyName("R")]
    WrittenOff = 10,

    [Description("Withdrawn from Use")]
    [JsonPropertyName("S")]
    WithdrawnfromUse = 11,

    [Description("Withdrawn from Use - Stored")]
    [JsonPropertyName("S")]
    WithdrawnfromUseStored = 12
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
