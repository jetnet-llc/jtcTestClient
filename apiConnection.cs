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
          Timeout = TimeSpan.FromSeconds(2100) // set the time out to 2100 seconds (35 minutes)
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
  public class Aircraft2
  {
    // Identification
    public int aircraftid { get; set; }
    public string? make { get; set; }
    public string? model { get; set; }
    public int modelid { get; set; }
    public string? maketype { get; set; }

    // Manufacturer and Registration
    public dynamic? mfr { get; set; }
    public dynamic? icao { get; set; }
    public dynamic? sernbr { get; set; }
    public dynamic? regnbr { get; set; }

    // Year Information
    public dynamic? yearmfr { get; set; }
    public dynamic? yeardlv { get; set; }

    // Sale Status
    public string? forsale { get; set; }
    public string? exclusive { get; set; }
    public string? leased { get; set; }
    public dynamic? marketstatus { get; set; }
    public dynamic? asking { get; set; }
    public dynamic? askingprice { get; set; }
    public dynamic? listdate { get; set; }

    // Base Location
    public string? basecity { get; set; }
    public string? basestate { get; set; }
    public string? basecountry { get; set; }
    public dynamic? baseairportid { get; set; }
    public dynamic? baseicaocode { get; set; }
    public dynamic? baseiatacode { get; set; }

    // Flight Time
    public dynamic? aftt { get; set; }
    public dynamic? estaftt { get; set; }

    // Engine Serial Numbers
    public dynamic? enginesn1 { get; set; }
    public dynamic? enginesn2 { get; set; }
  }

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
      basecountrylist = null;
      basecode = "";
      actiondate = "";
      companyid = 0;
      contactid = 0;
      yearmfr = 0;
      yeardlv = 0;
      aircraftchanges = null;
      aclist = null;
      modlist = null;
      exactMatchReg = false;
      showHistoricalAcRefs = false;
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
    public List<string>? basecountrylist { get; set; }
    public string basecode { get; set; }
    public string actiondate { get; set; }
    public int companyid { get; set; }
    public int contactid { get; set; }
    public int yearmfr { get; set; }
    public int yeardlv { get; set; }
    public string? aircraftchanges { get; set; }
    public List<int>? aclist { get; set; }
    public List<int>? modlist { get; set; }

    [DefaultValue(false)]
    public bool exactMatchReg { get; set; }

    [DefaultValue(false)]
    public bool showHistoricalAcRefs { get; set; }
  }
  public class responseAircraftList
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int count { get; set; }
    public string? pagelink { get; set; }
    public List<Aircraft2>? aircraft { get; set; }
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
    public int aircraftid { get; set; }
    public string? regnbr { get; set; }
    public int companyid { get; set; }
    public string? name { get; set; }
    public string? relationtype { get; set; }
    public int relationseqno { get; set; }
    public dynamic? contactid { get; set; }
    public dynamic? ownerpercent { get; set; }
    public dynamic? fractionexpiresdate { get; set; }
    public string? isoperator { get; set; }
    public string? businesstype { get; set; }
    public companyReportClass? company { get; set; }
    public contactReportClass? contact { get; set; }

    public compReferenceClass()
    {
      aircraftid = 0;
      regnbr = "";
      companyid = 0;
      relationtype = "";
      relationseqno = 0;
      contactid = null;
      ownerpercent = null;
      fractionexpiresdate = null;
      isoperator = "";
      businesstype = "";
      company = null;
      contact = null;
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
  public class aircraftPicturesClass
  {
    public int imageid { get; set; }
    public string? description { get; set; }
    public string? extension { get; set; }
    public string? pictureurl { get; set; }
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
    public List<aircraftPicturesClass>? pictures { get; set; }
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
      pictures = null;
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
  internal class responseGulfstreamExport
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int exportaircraftcount { get; set; }
    public int aircraftaddcount { get; set; }
    public List<string>? aircraftadd { get; set; }
    public int companyaddcount { get; set; }
    public List<string>? companyadd { get; set; }
    public int contactaddcount { get; set; }
    public List<string>? contactadd { get; set; }
    public int phoneaddcount { get; set; }
    public List<string>? phoneadd { get; set; }
    public int aircraftdeletecount { get; set; }
    public List<string>? aircraftdelete { get; set; }
    public int companydeletecount { get; set; }
    public List<string>? companydelete { get; set; }
    public int contactdeletecount { get; set; }
    public List<string>? contactdelete { get; set; }

  }
  public class fuelerLinxAcClass
  {
    public fuelerLinxAcClass()
    {
      ac_id = 0;
      amod_id = 0;
      ac_airframe_type_code = eAirFrameTypes.None;
      ac_maketype = eMakeTypes.None;
      amod_make_name = null;
      amod_model_name = null;
      ac_weightclass = "";
      ac_jniqcategory = null;
      amod_icao_code = "";
      ac_ser_no_full = null;
      ac_reg_no = null;
      ac_mfr_year = 0;
      ac_year = 0;
      ac_aport_icao_code = null;
      ac_aport_name = null;
      ac_ownership = "";
      ac_usage = "";
      ac_maintained = null;
      ac_ref_comp_types = null;

    }

    [JsonPropertyName("aircraftid")]
    public long ac_id { get; set; }

    [JsonPropertyName("modelid")]
    public long amod_id { get; set; }

    [JsonPropertyName("airframetype")]
    public eAirFrameTypes ac_airframe_type_code { get; set; }

    [JsonPropertyName("maketype")]
    public eMakeTypes ac_maketype { get; set; }

    [JsonPropertyName("make")]
    public dynamic? amod_make_name { get; set; }

    [JsonPropertyName("model")]
    public dynamic? amod_model_name { get; set; }

    [JsonPropertyName("icaotype")]
    public string? amod_icao_code { get; set; }

    [JsonPropertyName("serialnbr")]
    public dynamic? ac_ser_no_full { get; set; }

    [JsonPropertyName("regnbr")]
    public dynamic? ac_reg_no { get; set; }

    [JsonPropertyName("yearmfr")]
    public int ac_mfr_year { get; set; }

    [JsonPropertyName("yeardlv")]
    public int ac_year { get; set; }

    [JsonPropertyName("weightclass")]
    public string? ac_weightclass { get; set; }

    [JsonPropertyName("categorysize")]
    public dynamic? ac_jniqcategory { get; set; }

    [JsonPropertyName("baseicao")]
    public string? ac_aport_icao_code { get; set; }

    [JsonPropertyName("baseairport")]
    public string? ac_aport_name { get; set; }

    [JsonPropertyName("ownership")]
    public string? ac_ownership { get; set; }

    [JsonPropertyName("usage")]
    public string? ac_usage { get; set; }

    [JsonPropertyName("maintained")]
    public dynamic? ac_maintained { get; set; }

    // list of pre-defined contact type references
    [JsonPropertyName("companyrelationships")]
    public List<fuelerLinxAcRefClass>? ac_ref_comp_types { get; set; }
  }
  public class fuelerLinxAcRefClass
  {
    public fuelerLinxAcRefClass()
    {
      comp_id = 0;
      comp_parent_comp_id = 0;
      comp_relation = null;
      comp_agencytype = null;
      comp_businesstype = null;
      comp_name = null;
      comp_address1 = null;
      comp_address2 = null;
      comp_city = null;
      comp_state = null;
      comp_stateabbr = null;
      comp_postcode = null;
      comp_country = null;
      comp_email_address = null;
      comp_office = null;

      contact_id = 0;
      contact_sirname = null;
      contact_first_name = null;
      contact_middle_initial = null;
      contact_last_name = null;
      contact_suffix = null;
      contact_title = null;
      contact_email_address = null;
      contact_best = null;
      contact_office = null;
      contact_mobile = null;
    }

    [JsonPropertyName("companyid")]
    public long comp_id { get; set; }

    [JsonPropertyName("parentcompanyid")]
    public long comp_parent_comp_id { get; set; }

    [JsonPropertyName("companyrelation")]
    public dynamic? comp_relation { get; set; }

    [JsonPropertyName("companyname")]
    public dynamic? comp_name { get; set; }

    [JsonPropertyName("companyisoperator")]
    public dynamic? comp_operator_flag { get; set; }

    [JsonPropertyName("companyagencytype")]
    public dynamic? comp_agencytype { get; set; }

    [JsonPropertyName("companybusinesstype")]
    public dynamic? comp_businesstype { get; set; }

    [JsonPropertyName("companyaddress1")]
    public dynamic? comp_address1 { get; set; }

    [JsonPropertyName("companyaddress2")]
    public dynamic? comp_address2 { get; set; }

    [JsonPropertyName("companycity")]
    public dynamic? comp_city { get; set; }

    [JsonPropertyName("companystate")]
    public dynamic? comp_state { get; set; }

    [JsonPropertyName("companystateabbr")]
    public dynamic? comp_stateabbr { get; set; }

    [JsonPropertyName("companypostcode")]
    public dynamic? comp_postcode { get; set; }

    [JsonPropertyName("companycountry")]
    public dynamic? comp_country { get; set; }

    [JsonPropertyName("companyemail")]
    public dynamic? comp_email_address { get; set; }

    [JsonPropertyName("companyofficephone")]
    public dynamic? comp_office { get; set; }

    [JsonPropertyName("contactid")]
    public long contact_id { get; set; }

    [JsonPropertyName("contactsirname")]
    public dynamic? contact_sirname { get; set; }

    [JsonPropertyName("contactfirstname")]
    public dynamic? contact_first_name { get; set; }

    [JsonPropertyName("contactmiddleinitial")]
    public dynamic? contact_middle_initial { get; set; }

    [JsonPropertyName("contactlastname")]
    public dynamic? contact_last_name { get; set; }

    [JsonPropertyName("contactsuffix")]
    public dynamic? contact_suffix { get; set; }

    [JsonPropertyName("contacttitle")]
    public dynamic? contact_title { get; set; }

    [JsonPropertyName("contactemail")]
    public dynamic? contact_email_address { get; set; }

    [JsonPropertyName("contactbestphone")]
    public dynamic? contact_best { get; set; }

    [JsonPropertyName("contactofficephone")]
    public dynamic? contact_office { get; set; }

    [JsonPropertyName("contactmobilephone")]
    public dynamic? contact_mobile { get; set; }
  }
  public class responseAcPictures
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int count { get; set; }
    public List<aircraftPicturesClass>? pictures { get; set; }
  }
  public class responseFuelerLinxExport
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public fuelerLinxAcClass? aircraftresult { get; set; }
  }
  public class bulkAircraftRecordsClass
  {
    public bulkAircraftRecordsClass() // 285 properties
    {

      aircraftid = 0;
      modelid = null;
      airframetypecode = eAirFrameTypes.None;
      maketype = eMakeTypes.None;
      make = null;
      model = null;
      sernbr = null;
      regnbr = null;
      yeardelivered = null;
      yearmfr = null;
      forsale = null;
      asking = null;
      status = null;
      maintained = null;
      datepurchased = null;
      dateedited = null;
      datelisted = null;

      owrcompid = null; //
      owrcontactid = null;
      owrregascompid = null;
      owrdbnbr = null;
      owrtypecode = null;
      owrregisteredas = null;
      owrcompanyname = null;
      owraddress1 = null;
      owraddress2 = null;
      owrcity = null;
      owrstate = null;
      owrzip = null;
      owrcountry = null;
      owrphone1 = null;
      owrphone2 = null;
      owrsalut = null;
      owrfname = null;
      owrmiddle = null;
      owrlname = null;
      owrsuffix = null;
      owrtitle = null;
      owremail = null;

      oprcompid = null; //
      oprcontactid = null;
      oprdbnbr = null;
      oprtypecode = null;
      oprcompanyname = null;
      opraddress1 = null;
      opraddress2 = null;
      oprcity = null;
      oprstate = null;
      oprzip = null;
      oprcountry = null;
      oprphone1 = null;
      oprphone2 = null;
      oprsalut = null;
      oprfname = null;
      oprmiddle = null;
      oprlname = null;
      oprsuffix = null;
      oprtitle = null;
      opremail = null;

      chpcompid = null; //
      chpcontactid = null;
      chpdbnbr = null;
      chptypecode = null;
      chpcompanyname = null;
      chpaddress1 = null;
      chpaddress2 = null;
      chpcity = null;
      chpstate = null;
      chpzip = null;
      chpcountry = null;
      chpphone1 = null;
      chpphone2 = null;
      chpsalut = null;
      chpfname = null;
      chpmiddle = null;
      chplname = null;
      chpsuffix = null;
      chptitle = null;
      chpemail = null;

      excbrk1compid = null; //
      excbrk1contactid = null;
      excbrk1dbnbr = null;
      excbrk1typecode = null;
      excbrk1companyname = null;
      excbrk1address1 = null;
      excbrk1address2 = null;
      excbrk1city = null;
      excbrk1state = null;
      excbrk1zip = null;
      excbrk1country = null;
      excbrk1phone1 = null;
      excbrk1phone2 = null;
      excbrk1salut = null;
      excbrk1fname = null;
      excbrk1middle = null;
      excbrk1lname = null;
      excbrk1suffix = null;
      excbrk1title = null;
      excbrk1email = null; //

      excbrk2compid = null;
      excbrk2contactid = null;
      excbrk2dbnbr = null;
      excbrk2typecode = null;
      excbrk2companyname = null;
      excbrk2address1 = null;
      excbrk2address2 = null;
      excbrk2city = null;
      excbrk2state = null;
      excbrk2zip = null;
      excbrk2country = null;
      excbrk2phone1 = null;
      excbrk2phone2 = null;
      excbrk2salut = null;
      excbrk2fname = null;
      excbrk2middle = null;
      excbrk2lname = null;
      excbrk2suffix = null;
      excbrk2title = null;
      excbrk2email = null;

      addl1compid = null; //
      addl1contactid = null;
      addl1dbnbr = null;
      addl1typecode = null;
      addl1companyname = null;
      addl1address1 = null;
      addl1address2 = null;
      addl1city = null;
      addl1state = null;
      addl1zip = null;
      addl1country = null;
      addl1phone1 = null;
      addl1phone2 = null;
      addl1salut = null;
      addl1fname = null;
      addl1middle = null;
      addl1lname = null;
      addl1suffix = null;
      addl1title = null;
      addl1email = null;

      addl2compid = null; //
      addl2contactid = null;
      addl2dbnbr = null;
      addl2typecode = null;
      addl2companyname = null;
      addl2address1 = null;
      addl2address2 = null;
      addl2city = null;
      addl2state = null;
      addl2zip = null;
      addl2country = null;
      addl2phone1 = null;
      addl2phone2 = null;
      addl2salut = null;
      addl2fname = null;
      addl2middle = null;
      addl2lname = null;
      addl2suffix = null;
      addl2title = null;
      addl2email = null;

      addl3compid = null; // 
      addl3contactid = null;
      addl3dbnbr = null;
      addl3typecode = null;
      addl3companyname = null;
      addl3address1 = null;
      addl3address2 = null;
      addl3city = null;
      addl3state = null;
      addl3zip = null;
      addl3country = null;
      addl3phone1 = null;
      addl3phone2 = null;
      addl3salut = null;
      addl3fname = null;
      addl3middle = null;
      addl3lname = null;
      addl3suffix = null;
      addl3title = null;
      addl3email = null;

      acbasecode = null;
      acbaseiata = null;
      acbaseicao = null;
      acbasecity = null;
      acbasestate = null;
      acbasename = null;
      acbasecountry = null;

      adf = null;
      altimtr = null;
      ap = null;
      avncs = null;
      comms = null;
      compass = null;
      cvrfdr = null;
      dme = null;
      efis = null;
      fdir = null;
      fphone = null;
      hifreq = null;
      hsi = null;
      loran = null;
      lrn = null;
      navs = null;
      radar = null;
      rdralt = null;
      rmi = null;
      rnav = null;
      stormscope = null;
      tcas = null;
      tpdr = null;

      engmodel = null;
      engoh = null;
      mxprog = null;
      hots = null;
      soh = null;
      airfrmtt = null;
      ttsn = null;
      landings = null;

      engsn1 = null;
      engsn2 = null;
      engsn3 = null;
      engsn4 = null;
      engttl_new = null;
      engttr_new = null;
      engtt1_new = null;
      engtt2_new = null;
      shotl_new = null;
      shotr_new = null;
      shot1_new = null;
      shot2_new = null;
      smohl_new = null;
      smohr_new = null;
      smoh1_new = null;
      smoh2_new = null;
      spohl_new = null;
      spohr_new = null;
      spoh1_new = null;
      spoh2_new = null;
      tbol_new = null;
      tbor_new = null;
      tbo1_new = null;
      tbo2_new = null;

      passengers = null;
      apu = null;
      apusn = null;
      color = null;

      conf1 = null;
      conf2 = null;

      damagehistoryyn = null;
      emp = null;
      intrate = null;
      intyear = null;
      extrate = null;
      extyear = null;

      feature1 = null;
      feature1yesno = null;
      feature2 = null;
      feature2yesno = null;
      feature3 = null;
      feature3yesno = null;
      feature4 = null;
      feature4yesno = null;
      feature5 = null;
      feature5yesno = null;
      feature6 = null;
      feature6yesno = null;

      leased = null;
      leaseexpires = null;
      leasenote1 = null;
      leaseterm = null;
      leasetype = null;
      leasestartdate = null;

      writtenoffdate = null;
      writtenoffcategory = null;
      lifecycle = null;
      altsernbr = null;
      timescurrentdate = null;
    }

    public dynamic? aircraftid { get; set; }
    public dynamic? modelid { get; set; }

    [JsonPropertyName("maketype")]
    public eMakeTypes maketype { get; set; }

    [JsonPropertyName("airframetype")]
    public eAirFrameTypes airframetypecode { get; set; }

    public dynamic? make { get; set; }
    public dynamic? sernbr { get; set; }
    public dynamic? model { get; set; }
    public dynamic? regnbr { get; set; }
    public dynamic? yeardelivered { get; set; }
    public dynamic? yearmfr { get; set; }
    public dynamic? forsale { get; set; }
    public dynamic? asking { get; set; }
    public dynamic? status { get; set; }
    public dynamic? maintained { get; set; }
    public dynamic? datepurchased { get; set; }
    public dynamic? dateedited { get; set; }
    public dynamic? datelisted { get; set; }

    public dynamic? owrcompid { get; set; }
    public dynamic? owrcontactid { get; set; }
    public dynamic? owrregascompid { get; set; }
    public dynamic? owrdbnbr { get; set; }
    public dynamic? owrtypecode { get; set; }
    public dynamic? owrregisteredas { get; set; }
    public dynamic? owrcompanyname { get; set; }
    public dynamic? owraddress1 { get; set; }
    public dynamic? owraddress2 { get; set; }
    public dynamic? owrcity { get; set; }
    public dynamic? owrstate { get; set; }
    public dynamic? owrzip { get; set; }
    public dynamic? owrcountry { get; set; }
    public dynamic? owrphone1 { get; set; }
    public dynamic? owrphone2 { get; set; }
    public dynamic? owrsalut { get; set; }
    public dynamic? owrfname { get; set; }
    public dynamic? owrmiddle { get; set; }
    public dynamic? owrlname { get; set; }
    public dynamic? owrsuffix { get; set; }
    public dynamic? owrtitle { get; set; }
    public dynamic? owremail { get; set; }

    public dynamic? oprcompid { get; set; }
    public dynamic? oprcontactid { get; set; }
    public dynamic? oprdbnbr { get; set; }
    public dynamic? oprtypecode { get; set; }
    public dynamic? oprcompanyname { get; set; }
    public dynamic? opraddress1 { get; set; }
    public dynamic? opraddress2 { get; set; }
    public dynamic? oprcity { get; set; }
    public dynamic? oprstate { get; set; }
    public dynamic? oprzip { get; set; }
    public dynamic? oprcountry { get; set; }
    public dynamic? oprphone1 { get; set; }
    public dynamic? oprphone2 { get; set; }
    public dynamic? oprsalut { get; set; }
    public dynamic? oprfname { get; set; }
    public dynamic? oprmiddle { get; set; }
    public dynamic? oprlname { get; set; }
    public dynamic? oprsuffix { get; set; }
    public dynamic? oprtitle { get; set; }
    public dynamic? opremail { get; set; }

    public dynamic? chpcompid { get; set; }
    public dynamic? chpcontactid { get; set; }
    public dynamic? chpdbnbr { get; set; }
    public dynamic? chptypecode { get; set; }
    public dynamic? chpcompanyname { get; set; }
    public dynamic? chpaddress1 { get; set; }
    public dynamic? chpaddress2 { get; set; }
    public dynamic? chpcity { get; set; }
    public dynamic? chpstate { get; set; }
    public dynamic? chpzip { get; set; }
    public dynamic? chpcountry { get; set; }
    public dynamic? chpphone1 { get; set; }
    public dynamic? chpphone2 { get; set; }
    public dynamic? chpsalut { get; set; }
    public dynamic? chpfname { get; set; }
    public dynamic? chpmiddle { get; set; }
    public dynamic? chplname { get; set; }
    public dynamic? chpsuffix { get; set; }
    public dynamic? chptitle { get; set; }
    public dynamic? chpemail { get; set; }

    public dynamic? excbrk1compid { get; set; }
    public dynamic? excbrk1contactid { get; set; }
    public dynamic? excbrk1dbnbr { get; set; }
    public dynamic? excbrk1typecode { get; set; }
    public dynamic? excbrk1companyname { get; set; }
    public dynamic? excbrk1address1 { get; set; }
    public dynamic? excbrk1address2 { get; set; }
    public dynamic? excbrk1city { get; set; }
    public dynamic? excbrk1state { get; set; }
    public dynamic? excbrk1zip { get; set; }
    public dynamic? excbrk1country { get; set; }
    public dynamic? excbrk1phone1 { get; set; }
    public dynamic? excbrk1phone2 { get; set; }
    public dynamic? excbrk1salut { get; set; }
    public dynamic? excbrk1fname { get; set; }
    public dynamic? excbrk1middle { get; set; }
    public dynamic? excbrk1lname { get; set; }
    public dynamic? excbrk1suffix { get; set; }
    public dynamic? excbrk1title { get; set; }
    public dynamic? excbrk1email { get; set; }

    public dynamic? excbrk2compid { get; set; }
    public dynamic? excbrk2contactid { get; set; }
    public dynamic? excbrk2dbnbr { get; set; }
    public dynamic? excbrk2typecode { get; set; }
    public dynamic? excbrk2companyname { get; set; }
    public dynamic? excbrk2address1 { get; set; }
    public dynamic? excbrk2address2 { get; set; }
    public dynamic? excbrk2city { get; set; }
    public dynamic? excbrk2state { get; set; }
    public dynamic? excbrk2zip { get; set; }
    public dynamic? excbrk2country { get; set; }
    public dynamic? excbrk2phone1 { get; set; }
    public dynamic? excbrk2phone2 { get; set; }
    public dynamic? excbrk2salut { get; set; }
    public dynamic? excbrk2fname { get; set; }
    public dynamic? excbrk2middle { get; set; }
    public dynamic? excbrk2lname { get; set; }
    public dynamic? excbrk2suffix { get; set; }
    public dynamic? excbrk2title { get; set; }
    public dynamic? excbrk2email { get; set; }

    public dynamic? addl1compid { get; set; }
    public dynamic? addl1contactid { get; set; }
    public dynamic? addl1dbnbr { get; set; }
    public dynamic? addl1typecode { get; set; }
    public dynamic? addl1companyname { get; set; }
    public dynamic? addl1address1 { get; set; }
    public dynamic? addl1address2 { get; set; }
    public dynamic? addl1city { get; set; }
    public dynamic? addl1state { get; set; }
    public dynamic? addl1zip { get; set; }
    public dynamic? addl1country { get; set; }
    public dynamic? addl1phone1 { get; set; }
    public dynamic? addl1phone2 { get; set; }
    public dynamic? addl1salut { get; set; }
    public dynamic? addl1fname { get; set; }
    public dynamic? addl1middle { get; set; }
    public dynamic? addl1lname { get; set; }
    public dynamic? addl1suffix { get; set; }
    public dynamic? addl1title { get; set; }
    public dynamic? addl1email { get; set; }

    public dynamic? addl2compid { get; set; }
    public dynamic? addl2contactid { get; set; }
    public dynamic? addl2dbnbr { get; set; }
    public dynamic? addl2typecode { get; set; }
    public dynamic? addl2companyname { get; set; }
    public dynamic? addl2address1 { get; set; }
    public dynamic? addl2address2 { get; set; }
    public dynamic? addl2city { get; set; }
    public dynamic? addl2state { get; set; }
    public dynamic? addl2zip { get; set; }
    public dynamic? addl2country { get; set; }
    public dynamic? addl2phone1 { get; set; }
    public dynamic? addl2phone2 { get; set; }
    public dynamic? addl2salut { get; set; }
    public dynamic? addl2fname { get; set; }
    public dynamic? addl2middle { get; set; }
    public dynamic? addl2lname { get; set; }
    public dynamic? addl2suffix { get; set; }
    public dynamic? addl2title { get; set; }
    public dynamic? addl2email { get; set; }

    public dynamic? addl3compid { get; set; }
    public dynamic? addl3contactid { get; set; }
    public dynamic? addl3dbnbr { get; set; }
    public dynamic? addl3typecode { get; set; }
    public dynamic? addl3companyname { get; set; }
    public dynamic? addl3address1 { get; set; }
    public dynamic? addl3address2 { get; set; }
    public dynamic? addl3city { get; set; }
    public dynamic? addl3state { get; set; }
    public dynamic? addl3zip { get; set; }
    public dynamic? addl3country { get; set; }
    public dynamic? addl3phone1 { get; set; }
    public dynamic? addl3phone2 { get; set; }
    public dynamic? addl3salut { get; set; }
    public dynamic? addl3fname { get; set; }
    public dynamic? addl3middle { get; set; }
    public dynamic? addl3lname { get; set; }
    public dynamic? addl3suffix { get; set; }
    public dynamic? addl3title { get; set; }
    public dynamic? addl3email { get; set; }

    public dynamic? acbasecode { get; set; }
    public dynamic? acbaseiata { get; set; }
    public dynamic? acbaseicao { get; set; }
    public dynamic? acbasecity { get; set; }
    public dynamic? acbasestate { get; set; }
    public dynamic? acbasename { get; set; }
    public dynamic? acbasecountry { get; set; }

    public dynamic? adf { get; set; }
    public dynamic? altimtr { get; set; }
    public dynamic? ap { get; set; }
    public dynamic? avncs { get; set; }
    public dynamic? comms { get; set; }
    public dynamic? compass { get; set; }
    public dynamic? cvrfdr { get; set; }
    public dynamic? dme { get; set; }
    public dynamic? efis { get; set; }
    public dynamic? fdir { get; set; }
    public dynamic? fphone { get; set; }
    public dynamic? hifreq { get; set; }
    public dynamic? hsi { get; set; }
    public dynamic? loran { get; set; }
    public dynamic? lrn { get; set; }
    public dynamic? navs { get; set; }
    public dynamic? radar { get; set; }
    public dynamic? rdralt { get; set; }
    public dynamic? rmi { get; set; }
    public dynamic? rnav { get; set; }
    public dynamic? stormscope { get; set; }
    public dynamic? tcas { get; set; }
    public dynamic? tpdr { get; set; }

    public dynamic? engmodel { get; set; }
    public dynamic? engoh { get; set; }
    public dynamic? mxprog { get; set; }
    public dynamic? hots { get; set; }
    public dynamic? soh { get; set; }
    public dynamic? airfrmtt { get; set; }
    public dynamic? ttsn { get; set; }
    public dynamic? landings { get; set; }

    public dynamic? engsn1 { get; set; }
    public dynamic? engsn2 { get; set; }
    public dynamic? engsn3 { get; set; }
    public dynamic? engsn4 { get; set; }

    public dynamic? engttl_new { get; set; }
    public dynamic? engttr_new { get; set; }
    public dynamic? engtt1_new { get; set; }
    public dynamic? engtt2_new { get; set; }

    public dynamic? shotl_new { get; set; }
    public dynamic? shotr_new { get; set; }
    public dynamic? shot1_new { get; set; }
    public dynamic? shot2_new { get; set; }

    public dynamic? smohl_new { get; set; }
    public dynamic? smohr_new { get; set; }
    public dynamic? smoh1_new { get; set; }
    public dynamic? smoh2_new { get; set; }

    public dynamic? spohl_new { get; set; }
    public dynamic? spohr_new { get; set; }
    public dynamic? spoh1_new { get; set; }
    public dynamic? spoh2_new { get; set; }

    public dynamic? tbol_new { get; set; }
    public dynamic? tbor_new { get; set; }
    public dynamic? tbo1_new { get; set; }
    public dynamic? tbo2_new { get; set; }

    public dynamic? passengers { get; set; }
    public dynamic? airco { get; set; }
    public dynamic? apu { get; set; }
    public dynamic? apusn { get; set; }
    public dynamic? color { get; set; }

    //
    public dynamic? conf1 { get; set; }
    public dynamic? conf2 { get; set; }
    //

    public dynamic? damagehistoryyn { get; set; }
    public dynamic? emp { get; set; }

    public dynamic? intrate { get; set; }
    public dynamic? intyear { get; set; }

    public dynamic? extrate { get; set; }
    public dynamic? extyear { get; set; }

    public dynamic? feature1 { get; set; }
    public dynamic? feature1yesno { get; set; }
    public dynamic? feature2 { get; set; }
    public dynamic? feature2yesno { get; set; }
    public dynamic? feature3 { get; set; }
    public dynamic? feature3yesno { get; set; }
    public dynamic? feature4 { get; set; }
    public dynamic? feature4yesno { get; set; }
    public dynamic? feature5 { get; set; }
    public dynamic? feature5yesno { get; set; }
    public dynamic? feature6 { get; set; }
    public dynamic? feature6yesno { get; set; }

    public dynamic? leased { get; set; }
    public dynamic? leaseexpires { get; set; }
    public dynamic? leasenote1 { get; set; }
    public dynamic? leaseterm { get; set; }
    public dynamic? leasetype { get; set; }
    public dynamic? leasestartdate { get; set; }

    public dynamic? writtenoffdate { get; set; }
    public dynamic? writtenoffcategory { get; set; }

    [JsonPropertyName("lifecycle")]
    public dynamic? lifecycle { get; set; }

    public dynamic? altsernbr { get; set; }
    public dynamic? timescurrentdate { get; set; }

  }
  public class responseBulkExport
  {

    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int exportaircraftcount { get; set; }
    public int aircraftcount { get; set; }
    public List<bulkAircraftRecordsClass>? aircraft { get; set; }
    public int companycount { get; set; }
    public List<bulkCompanyRecordsClass>? companies { get; set; }
    public int contactcount { get; set; }
    public List<bulkContactRecordsClass>? contacts { get; set; }
    public int phonenumbercount { get; set; }
    public List<bulkPhoneRecordsClass>? phonenumbers { get; set; }

  }
  public class responseCondensedOwnerOperatorReport
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int count { get; set; }
    public List<condensedOwnerOperatorClass>? aircraftowneroperators { get; set; }
    public List<condensedOwnerAPUClass>? aircraftowneroperatorapu { get; set; }
    public List<condensedOwnerMaintenanceClass>? aircraftowneroperatormaintenance { get; set; }
    public List<condensedOwnerEngineClass>? aircraftowneroperatorengines { get; set; }
    public List<condensedOwnerDetailItemClass>? aircraftowneroperatorinteriors { get; set; }
    public List<condensedOwnerDetailItemClass>? aircraftowneroperatorexteriors { get; set; }
    public List<condensedOwnerAvionicsClass>? aircraftowneroperatoravionics { get; set; }
  }
  public class condensedOwnerOperatorClass
  {

    public condensedOwnerOperatorClass()
    {
      Acid = 0;
      Acjournid = 0;
      Amodairframetypecode = eAirFrameTypes.None;
      Amodtypecode = eMakeTypes.None;
      Amodmakename = null;
      Amodmodelname = null;
      Acmodelid = 0;
      Acsernofull = null;
      Acregno = null;
      Regexpiresdate = null;

      // NON Aerodex fields
      Acstatus = null;
      Acasking = null;
      Acaskingprice = null;
      Aclistdate = null;
      Acpurchasedate = null;
      Acupddate = null;

      Comp1id = 0;
      Comp1relation = null;
      Comp1agencytype = null;
      Comp1businesstype = null;
      Comp1name = null;
      Comp1altname = null;
      Comp1email = null;
      Comp1webaddress = null;
      Comp1address1 = null;
      Comp1address2 = null;
      Comp1city = null;
      Comp1state = null;
      Comp1stateabbr = null;
      Comp1postalcode = null;
      Comp1country = null;
      Comp1office = null;
      Comp1fax = null;
      Comp1mobile = null;
      Contact1id = 0;
      Contact1salutation = null;
      Contact1firstname = null;
      Contact1middle = null;
      Contact1lastname = null;
      Contact1suffix = null;
      Contact1title = null;   // 01/13/2003 - by david d. cruger; added contact title
      Contact1email = null;   // 01/29/2003 - by david d. cruger; added contact email
      Contact1office = null;
      Contact1fax = null;
      Contact1mobile = null;

      Comp2id = 0;
      Comp2relation = null;
      Comp2agencytype = null;
      Comp2businesstype = null;
      Comp2name = null;
      Comp2altname = null;
      Comp2email = null;
      Comp2webaddress = null;
      Comp2address1 = null;
      Comp2address2 = null;
      Comp2city = null;
      Comp2state = null;
      Comp2stateabbr = null;
      Comp2postalcode = null;
      Comp2country = null;
      Comp2office = null;
      Comp2fax = null;
      Comp2mobile = null;
      Contact2id = 0;
      Contact2salutation = null;
      Contact2firstname = null;
      Contact2middle = null;
      Contact2lastname = null;
      Contact2suffix = null;
      Contact2title = null;   // 02/23/2003 - by david d. cruger; added contact title
      Contact2email = null;   // 02/29/2003 - by david d. cruger; added contact email
      Contact2office = null;
      Contact2fax = null;
      Contact2mobile = null;

      Chpilotcompid = 0;
      Chpilotcomprelation = null;
      Chpilotcompagencytype = null;
      Chpilotcompbusinesstype = null;
      Chpilotcompname = null;
      Chpilotcompaltname = null;
      Chpilotcompemail = null;
      Chpilotcompwebaddress = null;
      Chpilotcompaddress1 = null;
      Chpilotcompaddress2 = null;
      Chpilotcompcity = null;
      Chpilotcompstate = null;
      Chpilotcompstateabbr = null;
      Chpilotcomppostalcode = null;
      Chpilotcompcountry = null;
      Chpilotcompoffice = null;
      Chpilotcompfax = null;
      Chpilotcompmobile = null;
      Chpilotid = 0;
      Chpilotsalutation = null;
      Chpilotfirstname = null;
      Chpilotmiddle = null;
      Chpilotlastname = null;
      Chpilotsuffix = null;
      Chpilottitle = null;   // 01/13/2003 - by david d. cruger; added contact title
      Chpilotemail = null;   // 01/29/2003 - by david d. cruger; added contact email
      Chpilotoffice = null;
      Chpilotfax = null;
      Chpilotmobile = null;

      // NON Aerodex fields
      Excbrokercompid = 0;
      Excbrokercomprelation = null;
      Excbrokercompagencytype = null;
      Excbrokercompbusinesstype = null;
      Excbrokercompname = null;
      Excbrokercompaltname = null;
      Excbrokercompemail = null;
      Excbrokercompwebaddress = null;
      Excbrokercompaddress1 = null;
      Excbrokercompaddress2 = null;
      Excbrokercompcity = null;
      Excbrokercompstate = null;
      Excbrokercompstateabbr = null;
      Excbrokercomppostalcode = null;
      Excbrokercompcountry = null;
      Excbrokercompoffice = null;
      Excbrokercompfax = null;
      Excbrokercompmobile = null;
      Excbrokerid = 0;
      Excbrokersalutation = null;
      Excbrokerfirstname = null;
      Excbrokermiddle = null;
      Excbrokerlastname = null;
      Excbrokersuffix = null;
      Excbrokertitle = null;   // 01/13/2003 - by david d. cruger; added contact title
      Excbrokeremail = null;   // 01/29/2003 - by david d. cruger; added contact email
      Excbrokeroffice = null;
      Excbrokerfax = null;
      Excbrokermobile = null;

      Acmfryear = 0;
      Acyear = 0;
      Acaportiatacode = null;
      Acaporticaocode = null;
      Acaportname = null;
      Acaportcity = null;
      Acaportstate = null;
      Acaportcountry = null;

      Acairframetothrs = 0;
      Acairframetotlandings = 0;
      Mxprog = null;
      Acpassengercount = 0;
      Acenginename = null;

      Acengine1serno = null;
      Acengine2serno = null;
      Acengine3serno = null;
      Acengine4serno = null;

      Acengine1tothrs = 0;
      Acengine2tothrs = 0;
      Acengine3tothrs = 0;
      Acengine4tothrs = 0;

      Acengine1sohhrs = 0;
      Acengine2sohhrs = 0;
      Acengine3sohhrs = 0;
      Acengine4sohhrs = 0;
      Acmainteohbyname = null;

      Acengine1shihrs = 0;
      Acengine2shihrs = 0;
      Acengine3shihrs = 0;
      Acengine4shihrs = 0;
      Acmainthotsbyname = null;

      Acengine1tbohrs = 0;
      Acengine2tbohrs = 0;
      Acengine3tbohrs = 0;
      Acengine4tbohrs = 0;

      Acavionicsavpackage = null;
      Acavionicsflightdir = null;
      Acavionicsautopilot = null;
      Acavionicsafis = null;
      Acavionicsfms = null;
      Acavionicsgps = null;
      Acavionicsins = null;
      Acavionicsirs = null;
      Acmaintained = null;

      Ifc1 = null;
      Ifc1yn = null;
      Ifc2 = null;
      Ifc2yn = null;
      Ifc3 = null;
      Ifc3yn = null;
      Ifc4 = null;
      Ifc4yn = null;
      Ifc5 = null;
      Ifc5yn = null;
      Ifc6 = null;
      Ifc6yn = null;

      Emp = null;
      Actimesasofdate = null;
      Acexteriormoyear = null;
      Acinteriormoyear = null;

      Aclifecyclestage = null;

      Infavorof = null;
      Docdate = null;
      Doctype = null;
      Docamount = null;

      Lessor = null;

    }

    [JsonPropertyName("acid")]
    public long Acid { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    [JsonPropertyName("acjournid")]
    public long Acjournid { get; set; }

    [JsonPropertyName("airframetype")]
    public eAirFrameTypes Amodairframetypecode { get; set; }

    [JsonPropertyName("maketype")]
    public eMakeTypes Amodtypecode { get; set; }

    [JsonPropertyName("make")]
    public dynamic? Amodmakename { get; set; }

    [JsonPropertyName("model")]
    public dynamic? Amodmodelname { get; set; }

    [JsonPropertyName("modelid")]
    public long Acmodelid { get; set; }

    [JsonPropertyName("serialnbr")]
    public dynamic? Acsernofull { get; set; }

    [JsonPropertyName("regnbr")]
    public dynamic? Acregno { get; set; }

    [JsonPropertyName("regexpiresdate")]
    public dynamic? Regexpiresdate { get; set; }
    // NON Aerodex fields

    [JsonPropertyName("status")]
    public dynamic? Acstatus { get; set; }

    [JsonPropertyName("asking")]
    public dynamic? Acasking { get; set; }

    [JsonPropertyName("askingamt")]
    public dynamic? Acaskingprice { get; set; }

    [JsonPropertyName("datelisted")]
    public dynamic? Aclistdate { get; set; }

    [JsonPropertyName("datepurchased")]
    public dynamic? Acpurchasedate { get; set; }

    [JsonPropertyName("datechanged")]
    public dynamic? Acupddate { get; set; }


    // comp 1 contact 1
    [JsonPropertyName("comp1id")]
    public long Comp1id { get; set; }

    [JsonPropertyName("comp1relation")]
    public dynamic? Comp1relation { get; set; }

    [JsonPropertyName("comp1agencytype")]
    public dynamic? Comp1agencytype { get; set; }

    [JsonPropertyName("comp1businesstype")]
    public dynamic? Comp1businesstype { get; set; }

    [JsonPropertyName("comp1name")]
    public dynamic? Comp1name { get; set; }

    [JsonPropertyName("comp1altname")]
    public dynamic? Comp1altname { get; set; }

    [JsonPropertyName("comp1email")]
    public dynamic? Comp1email { get; set; }

    [JsonPropertyName("comp1webaddress")]
    public dynamic? Comp1webaddress { get; set; }

    [JsonPropertyName("comp1address1")]
    public dynamic? Comp1address1 { get; set; }

    [JsonPropertyName("comp1address2")]
    public dynamic? Comp1address2 { get; set; }

    [JsonPropertyName("comp1city")]
    public dynamic? Comp1city { get; set; }

    [JsonPropertyName("comp1state")]
    public dynamic? Comp1state { get; set; }

    [JsonPropertyName("comp1stateabbr")]
    public dynamic? Comp1stateabbr { get; set; }

    [JsonPropertyName("comp1postalcode")]
    public dynamic? Comp1postalcode { get; set; }

    [JsonPropertyName("comp1country")]
    public dynamic? Comp1country { get; set; }

    [JsonPropertyName("comp1office")]
    public dynamic? Comp1office { get; set; }

    [JsonPropertyName("comp1fax")]
    public dynamic? Comp1fax { get; set; }

    [JsonPropertyName("comp1mobile")]
    public dynamic? Comp1mobile { get; set; }

    [JsonPropertyName("contact1id")]
    public long Contact1id { get; set; }

    [JsonPropertyName("contact1salutation")]
    public dynamic? Contact1salutation { get; set; }

    [JsonPropertyName("contact1firstname")]
    public dynamic? Contact1firstname { get; set; }

    [JsonPropertyName("contact1middle")]
    public dynamic? Contact1middle { get; set; }

    [JsonPropertyName("contact1lastname")]
    public dynamic? Contact1lastname { get; set; }

    [JsonPropertyName("contact1suffix")]
    public dynamic? Contact1suffix { get; set; }

    [JsonPropertyName("contact1title")]
    public dynamic? Contact1title { get; set; }   // 01/13/2003 - by david d. cruger; added contact title

    [JsonPropertyName("contact1email")]
    public dynamic? Contact1email { get; set; }   // 01/29/2003 - by david d. cruger; added contact email

    [JsonPropertyName("contact1office")]
    public dynamic? Contact1office { get; set; }

    [JsonPropertyName("contact1fax")]
    public dynamic? Contact1fax { get; set; }

    [JsonPropertyName("contact1mobile")]
    public dynamic? Contact1mobile { get; set; }

    // comp 2 contatct 2
    [JsonPropertyName("comp2id")]
    public long Comp2id { get; set; }

    [JsonPropertyName("comp2relation")]
    public dynamic? Comp2relation { get; set; }

    [JsonPropertyName("comp2agencytype")]
    public dynamic? Comp2agencytype { get; set; }

    [JsonPropertyName("comp2businesstype")]
    public dynamic? Comp2businesstype { get; set; }

    [JsonPropertyName("comp2name")]
    public dynamic? Comp2name { get; set; }

    [JsonPropertyName("comp2altname")]
    public dynamic? Comp2altname { get; set; }

    [JsonPropertyName("comp2email")]
    public dynamic? Comp2email { get; set; }

    [JsonPropertyName("comp2webaddress")]
    public dynamic? Comp2webaddress { get; set; }

    [JsonPropertyName("comp2address1")]
    public dynamic? Comp2address1 { get; set; }

    [JsonPropertyName("comp2address2")]
    public dynamic? Comp2address2 { get; set; }

    [JsonPropertyName("comp2city")]
    public dynamic? Comp2city { get; set; }

    [JsonPropertyName("comp2state")]
    public dynamic? Comp2state { get; set; }

    [JsonPropertyName("comp2stateabbr")]
    public dynamic? Comp2stateabbr { get; set; }

    [JsonPropertyName("comp2postalcode")]
    public dynamic? Comp2postalcode { get; set; }

    [JsonPropertyName("comp2country")]
    public dynamic? Comp2country { get; set; }

    [JsonPropertyName("comp2office")]
    public dynamic? Comp2office { get; set; }

    [JsonPropertyName("comp2fax")]
    public dynamic? Comp2fax { get; set; }

    [JsonPropertyName("comp2mobile")]
    public dynamic? Comp2mobile { get; set; }

    [JsonPropertyName("contact2id")]
    public long Contact2id { get; set; }

    [JsonPropertyName("contact2salutation")]
    public dynamic? Contact2salutation { get; set; }

    [JsonPropertyName("contact2firstname")]
    public dynamic? Contact2firstname { get; set; }

    [JsonPropertyName("contact2middle")]
    public dynamic? Contact2middle { get; set; }

    [JsonPropertyName("contact2lastname")]
    public dynamic? Contact2lastname { get; set; }

    [JsonPropertyName("contact2suffix")]
    public dynamic? Contact2suffix { get; set; }

    [JsonPropertyName("contact2title")]
    public dynamic? Contact2title { get; set; }   // 02/23/2003 - by david d. cruger; added contact title

    [JsonPropertyName("contact2email")]
    public dynamic? Contact2email { get; set; }   // 02/29/2003 - by david d. cruger; added contact email

    [JsonPropertyName("contact2office")]
    public dynamic? Contact2office { get; set; }

    [JsonPropertyName("contact2fax")]
    public dynamic? Contact2fax { get; set; }

    [JsonPropertyName("contact2mobile")]
    public dynamic? Contact2mobile { get; set; }


    // chief pilot comp and contact
    [JsonPropertyName("chpilotcompid")]
    public long Chpilotcompid { get; set; }

    [JsonPropertyName("chpilotcomprelation")]
    public dynamic? Chpilotcomprelation { get; set; }

    [JsonPropertyName("chpilotcompagencytype")]
    public dynamic? Chpilotcompagencytype { get; set; }

    [JsonPropertyName("chpilotcompbusinesstype")]
    public dynamic? Chpilotcompbusinesstype { get; set; }

    [JsonPropertyName("chpilotcompname")]
    public dynamic? Chpilotcompname { get; set; }

    [JsonPropertyName("chpilotcompaltname")]
    public dynamic? Chpilotcompaltname { get; set; }

    [JsonPropertyName("chpilotcompemail")]
    public dynamic? Chpilotcompemail { get; set; }

    [JsonPropertyName("chpilotcompwebaddress")]
    public dynamic? Chpilotcompwebaddress { get; set; }

    [JsonPropertyName("chpilotcompaddress1")]
    public dynamic? Chpilotcompaddress1 { get; set; }

    [JsonPropertyName("chpilotcompaddress2")]
    public dynamic? Chpilotcompaddress2 { get; set; }

    [JsonPropertyName("chpilotcompcity")]
    public dynamic? Chpilotcompcity { get; set; }

    [JsonPropertyName("chpilotcompstate")]
    public dynamic? Chpilotcompstate { get; set; }

    [JsonPropertyName("chpilotcompstateabbr")]
    public dynamic? Chpilotcompstateabbr { get; set; }

    [JsonPropertyName("chpilotcomppostalcode")]
    public dynamic? Chpilotcomppostalcode { get; set; }

    [JsonPropertyName("chpilotcompcountry")]
    public dynamic? Chpilotcompcountry { get; set; }

    [JsonPropertyName("chpilotcompoffice")]
    public dynamic? Chpilotcompoffice { get; set; }

    [JsonPropertyName("chpilotcompfax")]
    public dynamic? Chpilotcompfax { get; set; }

    [JsonPropertyName("chpilotcompmobile")]
    public dynamic? Chpilotcompmobile { get; set; }

    [JsonPropertyName("chpilotid")]
    public long Chpilotid { get; set; }

    [JsonPropertyName("chpilotsalutation")]
    public dynamic? Chpilotsalutation { get; set; }

    [JsonPropertyName("chpilotfirstname")]
    public dynamic? Chpilotfirstname { get; set; }

    [JsonPropertyName("chpilotmiddle")]
    public dynamic? Chpilotmiddle { get; set; }

    [JsonPropertyName("chpilotlastname")]
    public dynamic? Chpilotlastname { get; set; }

    [JsonPropertyName("chpilotsuffix")]
    public dynamic? Chpilotsuffix { get; set; }

    [JsonPropertyName("chpilottitle")]
    public dynamic? Chpilottitle { get; set; }   // 01/13/2003 - by david d. cruger; added contact title

    [JsonPropertyName("chpilotemail")]
    public dynamic? Chpilotemail { get; set; }   // 01/29/2003 - by david d. cruger; added contact email

    [JsonPropertyName("chpilotoffice")]
    public dynamic? Chpilotoffice { get; set; }

    [JsonPropertyName("chpilotfax")]
    public dynamic? Chpilotfax { get; set; }

    [JsonPropertyName("chpilotmobile")]
    public dynamic? Chpilotmobile { get; set; }


    // NON Aerodex fields
    [JsonPropertyName("excbrokercompid")]
    public long Excbrokercompid { get; set; }

    [JsonPropertyName("excbrokercomprelation")]
    public dynamic? Excbrokercomprelation { get; set; }

    [JsonPropertyName("excbrokercompagencytype")]
    public dynamic? Excbrokercompagencytype { get; set; }

    [JsonPropertyName("excbrokercompbusinesstype")]
    public dynamic? Excbrokercompbusinesstype { get; set; }

    [JsonPropertyName("excbrokercompname")]
    public dynamic? Excbrokercompname { get; set; }

    [JsonPropertyName("excbrokercompaltname")]
    public dynamic? Excbrokercompaltname { get; set; }

    [JsonPropertyName("excbrokercompemail")]
    public dynamic? Excbrokercompemail { get; set; }

    [JsonPropertyName("excbrokercompwebaddress")]
    public dynamic? Excbrokercompwebaddress { get; set; }

    [JsonPropertyName("excbrokercompaddress1")]
    public dynamic? Excbrokercompaddress1 { get; set; }

    [JsonPropertyName("excbrokercompaddress2")]
    public dynamic? Excbrokercompaddress2 { get; set; }

    [JsonPropertyName("excbrokercompcity")]
    public dynamic? Excbrokercompcity { get; set; }

    [JsonPropertyName("excbrokercompstate")]
    public dynamic? Excbrokercompstate { get; set; }

    [JsonPropertyName("excbrokercompstateabbr")]
    public dynamic? Excbrokercompstateabbr { get; set; }

    [JsonPropertyName("excbrokercomppostalcode")]
    public dynamic? Excbrokercomppostalcode { get; set; }

    [JsonPropertyName("excbrokercompcountry")]
    public dynamic? Excbrokercompcountry { get; set; }

    [JsonPropertyName("excbrokercompoffice")]
    public dynamic? Excbrokercompoffice { get; set; }

    [JsonPropertyName("excbrokercompfax")]
    public dynamic? Excbrokercompfax { get; set; }

    [JsonPropertyName("excbrokercompmobile")]
    public dynamic? Excbrokercompmobile { get; set; }

    [JsonPropertyName("excbrokerid")]
    public long Excbrokerid { get; set; }

    [JsonPropertyName("excbrokersalutation")]
    public dynamic? Excbrokersalutation { get; set; }

    [JsonPropertyName("excbrokerfirstname")]
    public dynamic? Excbrokerfirstname { get; set; }

    [JsonPropertyName("excbrokermiddle")]
    public dynamic? Excbrokermiddle { get; set; }

    [JsonPropertyName("excbrokerlastname")]
    public dynamic? Excbrokerlastname { get; set; }

    [JsonPropertyName("excbrokersuffix")]
    public dynamic? Excbrokersuffix { get; set; }

    [JsonPropertyName("excbrokertitle")]
    public dynamic? Excbrokertitle { get; set; }   // 01/13/2003 - by david d. cruger; added contact title

    [JsonPropertyName("excbrokeremail")]
    public dynamic? Excbrokeremail { get; set; }   // 01/29/2003 - by david d. cruger; added contact email

    [JsonPropertyName("excbrokeroffice")]
    public dynamic? Excbrokeroffice { get; set; }

    [JsonPropertyName("excbrokerfax")]
    public dynamic? Excbrokerfax { get; set; }

    [JsonPropertyName("excbrokermobile")]
    public dynamic? Excbrokermobile { get; set; }


    [JsonPropertyName("yearmfg")]
    public int Acmfryear { get; set; }

    [JsonPropertyName("yeardlv")]
    public int Acyear { get; set; }

    [JsonPropertyName("baseiata")]
    public dynamic? Acaportiatacode { get; set; }

    [JsonPropertyName("baseicao")]
    public dynamic? Acaporticaocode { get; set; }

    [JsonPropertyName("basename")]
    public dynamic? Acaportname { get; set; }

    [JsonPropertyName("basecity")]
    public dynamic? Acaportcity { get; set; }

    [JsonPropertyName("basestate")]
    public dynamic? Acaportstate { get; set; }

    [JsonPropertyName("basecountry")]
    public dynamic? Acaportcountry { get; set; }


    [JsonPropertyName("aftt")]
    public int Acairframetothrs { get; set; }

    [JsonPropertyName("landings")]
    public int Acairframetotlandings { get; set; }

    [JsonPropertyName("mxprog")]
    public dynamic? Mxprog { get; set; }

    [JsonPropertyName("passgr")]
    public int Acpassengercount { get; set; }

    [JsonPropertyName("engmodel")]
    public dynamic? Acenginename { get; set; }



    [JsonPropertyName("engsernbr1")]
    public dynamic? Acengine1serno { get; set; }

    [JsonPropertyName("engsernbr2")]
    public dynamic? Acengine2serno { get; set; }

    [JsonPropertyName("engsernbr3")]
    public dynamic? Acengine3serno { get; set; }

    [JsonPropertyName("engsernbr4")]
    public dynamic? Acengine4serno { get; set; }


    [JsonPropertyName("eng1tt")]
    public int Acengine1tothrs { get; set; }

    [JsonPropertyName("eng2tt")]
    public int Acengine2tothrs { get; set; }

    [JsonPropertyName("eng3tt")]
    public int Acengine3tothrs { get; set; }

    [JsonPropertyName("eng4tt")]
    public int Acengine4tothrs { get; set; }


    [JsonPropertyName("smoh1")]
    public int Acengine1sohhrs { get; set; }

    [JsonPropertyName("smoh2")]
    public int Acengine2sohhrs { get; set; }

    [JsonPropertyName("smoh3")]
    public int Acengine3sohhrs { get; set; }

    [JsonPropertyName("smoh4")]
    public int Acengine4sohhrs { get; set; }

    [JsonPropertyName("eohby")]
    public dynamic? Acmainteohbyname { get; set; }


    [JsonPropertyName("shot1")]
    public int Acengine1shihrs { get; set; }

    [JsonPropertyName("shot2")]
    public int Acengine2shihrs { get; set; }

    [JsonPropertyName("shot3")]
    public int Acengine3shihrs { get; set; }

    [JsonPropertyName("shot4")]
    public int Acengine4shihrs { get; set; }

    [JsonPropertyName("hotsby")]
    public dynamic? Acmainthotsbyname { get; set; }


    [JsonPropertyName("tbo1")]
    public int Acengine1tbohrs { get; set; }

    [JsonPropertyName("tbo2")]
    public int Acengine2tbohrs { get; set; }

    [JsonPropertyName("tbo3")]
    public int Acengine3tbohrs { get; set; }

    [JsonPropertyName("tbo4")]
    public int Acengine4tbohrs { get; set; }


    [JsonPropertyName("avionics")]
    public dynamic? Acavionicsavpackage { get; set; }

    [JsonPropertyName("fdir")]
    public dynamic? Acavionicsflightdir { get; set; }

    [JsonPropertyName("ap")]
    public dynamic? Acavionicsautopilot { get; set; }

    [JsonPropertyName("afis")]
    public dynamic? Acavionicsafis { get; set; }

    [JsonPropertyName("fms")]
    public dynamic? Acavionicsfms { get; set; }

    [JsonPropertyName("gps")]
    public dynamic? Acavionicsgps { get; set; }

    [JsonPropertyName("ins")]
    public dynamic? Acavionicsins { get; set; }

    [JsonPropertyName("irs")]
    public dynamic? Acavionicsirs { get; set; }

    [JsonPropertyName("maintained")]
    public dynamic? Acmaintained { get; set; }


    [JsonPropertyName("ifc1")]
    public dynamic? Ifc1 { get; set; }

    [JsonPropertyName("ifc1yn")]
    public dynamic? Ifc1yn { get; set; }

    [JsonPropertyName("ifc2")]
    public dynamic? Ifc2 { get; set; }

    [JsonPropertyName("ifc2yn")]
    public dynamic? Ifc2yn { get; set; }

    [JsonPropertyName("ifc3")]
    public dynamic? Ifc3 { get; set; }

    [JsonPropertyName("ifc3yn")]
    public dynamic? Ifc3yn { get; set; }

    [JsonPropertyName("ifc4")]
    public dynamic? Ifc4 { get; set; }

    [JsonPropertyName("ifc4yn")]
    public dynamic? Ifc4yn { get; set; }

    [JsonPropertyName("ifc5")]
    public dynamic? Ifc5 { get; set; }

    [JsonPropertyName("ifc5yn")]
    public dynamic? Ifc5yn { get; set; }

    [JsonPropertyName("ifc6")]
    public dynamic? Ifc6 { get; set; }

    [JsonPropertyName("ifc6yn")]
    public dynamic? Ifc6yn { get; set; }


    [JsonPropertyName("emp")]
    public dynamic? Emp { get; set; }

    [JsonPropertyName("tcdate")]
    public dynamic? Actimesasofdate { get; set; }

    [JsonPropertyName("extyr")]
    public dynamic? Acexteriormoyear { get; set; }

    [JsonPropertyName("intyr")]
    public dynamic? Acinteriormoyear { get; set; }


    [JsonPropertyName("lifecycle")]
    public dynamic? Aclifecyclestage { get; set; }


    [JsonPropertyName("infavorof")]
    public dynamic? Infavorof { get; set; }

    [JsonPropertyName("docdate")]
    public dynamic? Docdate { get; set; }

    [JsonPropertyName("doctype")]
    public dynamic? Doctype { get; set; }

    [JsonPropertyName("docamount")]
    public dynamic? Docamount { get; set; }


    [JsonPropertyName("lessor")]
    public dynamic? Lessor { get; set; }

  }
  public class condensedOwnerAvionicsClass
  {
    [JsonPropertyName("aircraftid")]
    public long ac_id { get; set; }

    [JsonPropertyName("aircraftAvionics")]
    public List<aircraftAvionicsClass>? ac_avionics { get; set; }

    public condensedOwnerAvionicsClass()
    {
      ac_id = 0;
      ac_avionics = null;
    }

  }
  public class condensedOwnerDetailItemClass
  {
    [JsonPropertyName("aircraftid")]
    public long ac_id { get; set; }

    [JsonPropertyName("aircraftDetails")]
    public List<detailItemClass>? ac_details { get; set; }

    public condensedOwnerDetailItemClass()
    {
      ac_id = 0;
      ac_details = null;
    }

    public condensedOwnerDetailItemClass(long ac_id, List<detailItemClass> ac_details)
    {
      this.ac_id = ac_id;
      this.ac_details = ac_details;

    }
  }
  public class condensedOwnerAPUClass
  {
    [JsonPropertyName("aircraftid")]
    public long apu_ac_id { get; set; }

    [JsonPropertyName("model")]
    public string apu_model { get; set; }

    [JsonPropertyName("sernbr")]
    public string apu_serial_number { get; set; }

    [JsonPropertyName("ttsnew")]
    public dynamic? apu_ttsnew { get; set; }

    [JsonPropertyName("soh")]
    public dynamic? apu_soh { get; set; }

    [JsonPropertyName("maintenanceprogram")]
    public string apu_maintenanceprogram { get; set; }

    public condensedOwnerAPUClass()
    {
      apu_ac_id = 0;
      apu_model = "";
      apu_serial_number = "";
      apu_ttsnew = null;
      apu_soh = null;
      apu_maintenanceprogram = "";
    }

  }
  public class condensedOwnerEngineClass
  {

    [JsonPropertyName("aircraftid")]
    public long eng_ac_id { get; set; }

    [JsonPropertyName("onconditiontbo")]
    public dynamic? eng_onconditiontbo { get; set; }

    [JsonPropertyName("maintenanceprogram")]
    public string? eng_maintenanceprogram { get; set; }

    [JsonPropertyName("model")]
    public string? eng_model { get; set; }

    [JsonPropertyName("enginenoiserating")]
    public dynamic? ac_engine_noise_rating { get; set; }

    [JsonPropertyName("engines")]
    public List<engineClass>? ac_engines { get; set; }

    public condensedOwnerEngineClass()
    {
      eng_ac_id = 0;
      eng_onconditiontbo = null;
      eng_maintenanceprogram = "";
      eng_model = "";
      ac_engine_noise_rating = null;
      ac_engines = null;
    }

  }
  public class condensedOwnerMaintenanceClass
  {

    [JsonPropertyName("aircraftid")]
    public long ac_id { get; set; }

    [JsonPropertyName("maintained")]
    public string ac_maintained { get; set; }

    [JsonPropertyName("airframemaintenanceprogram")]
    public string ac_amp { get; set; }

    [JsonPropertyName("airframetrackingprogram")]
    public string ac_atp { get; set; }

    [JsonPropertyName("certifications")]
    public List<string>? ac_certifications { get; set; }

    [JsonPropertyName("notes")]
    public string? ac_notes { get; set; }

    [JsonPropertyName("weightscapacity")]
    public string? ac_weightscapacity { get; set; }

    [JsonPropertyName("damagestatus")]
    public dynamic? ac_damage_flag { get; set; }

    [JsonPropertyName("damagenotes")]
    public dynamic? ac_damage_history_notes { get; set; }

    [JsonPropertyName("maintenanceitems")]
    public List<maintItemClass>? ac_maint_items { get; set; }

    public condensedOwnerMaintenanceClass()
    {
      ac_id = 0;
      ac_maintained = "";
      ac_amp = "";
      ac_atp = "";
      ac_certifications = null;

      ac_notes = null;
      ac_weightscapacity = null;

      ac_damage_flag = null;
      ac_damage_history_notes = null;

      ac_maint_items = null;

    }

  }
  public class AcSnapshotOptions
  {
    public AcSnapshotOptions()
    {
      airframetype = eAirFrameTypes.None;
      maketype = eMakeTypes.None;
      sernbr = "";
      regnbr = "";
      regnbrlist = null;
      modelid = 0;
      make = "";
      makelist = null;
      lifecycle = eLifeCycle.None;
      basestate = null;
      basecountry = "";
      basecountrylist = null;
      basecode = "";
      startdate = "";
      enddate = "";
      yearmfr = 0;
      yeardlv = 0;
      aclist = null;
      modlist = null;
      exactMatchReg = false;
      displayRange = 1; // default to 1 month(s) display range
      summaryType = eModelTrendSummary.SumMonths; // default to monthly
    }

    public eAirFrameTypes airframetype { get; set; }
    public eMakeTypes maketype { get; set; }
    public string? sernbr { get; set; }
    public string? regnbr { get; set; }
    public List<string>? regnbrlist { get; set; }
    public int modelid { get; set; }
    public string? make { get; set; }
    public List<string>? makelist { get; set; }
    public eLifeCycle lifecycle { get; set; }
    public List<string>? basestate { get; set; }
    public string? basecountry { get; set; }
    public List<string>? basecountrylist { get; set; }
    public string? basecode { get; set; }

    [JsonPropertyName("snapshotdate")]
    public string? startdate { get; set; }
    public string? enddate { get; set; }
    public int yearmfr { get; set; }
    public int yeardlv { get; set; }
    public List<int>? aclist { get; set; }
    public List<int>? modlist { get; set; }

    [DefaultValue(false)]
    public bool exactMatchReg { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public int displayRange { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public eModelTrendSummary summaryType { get; set; }
  }
  public class responseCondensedSnapshot
  {

    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int count { get; set; }
    public int currentpage { get; set; }
    public int maxpages { get; set; }
    public List<condensedSnapshotClass>? snapshotowneroperators { get; set; }

  }
  public class condensedSnapshotClass
  {

    public condensedSnapshotClass()
    {
      Snapyear_month = string.Empty;
      Snapyear = 0;
      Snapmonth = 0;
      Acid = 0;
      Acjournid = 0;
      Amodairframetypecode = eAirFrameTypes.None;
      Amodtypecode = eMakeTypes.None;
      Amodmakename = null;
      Amodmodelname = null;
      Acmodelid = 0;
      Acsernofull = null;
      Acregno = null;
      Regexpiresdate = null;

      // NON Aerodex fields
      Acstatus = null;
      Acasking = null;
      Acaskingprice = null;
      Aclistdate = null;
      Acpurchasedate = null;
      Acupddate = null;

      Comp1id = 0;
      Comp1relation = null;
      Comp1agencytype = null;
      Comp1businesstype = null;
      Comp1name = null;
      Comp1altname = null;
      Comp1email = null;
      Comp1webaddress = null;
      Comp1address1 = null;
      Comp1address2 = null;
      Comp1city = null;
      Comp1state = null;
      Comp1stateabbr = null;
      Comp1postalcode = null;
      Comp1country = null;
      Comp1office = null;
      Comp1fax = null;
      Comp1mobile = null;

      Comp2id = 0;
      Comp2relation = null;
      Comp2agencytype = null;
      Comp2businesstype = null;
      Comp2name = null;
      Comp2altname = null;
      Comp2email = null;
      Comp2webaddress = null;
      Comp2address1 = null;
      Comp2address2 = null;
      Comp2city = null;
      Comp2state = null;
      Comp2stateabbr = null;
      Comp2postalcode = null;
      Comp2country = null;
      Comp2office = null;
      Comp2fax = null;
      Comp2mobile = null;

      Chpilotcompid = 0;
      Chpilotcomprelation = null;
      Chpilotcompagencytype = null;
      Chpilotcompbusinesstype = null;
      Chpilotcompname = null;
      Chpilotcompaltname = null;
      Chpilotcompemail = null;
      Chpilotcompwebaddress = null;
      Chpilotcompaddress1 = null;
      Chpilotcompaddress2 = null;
      Chpilotcompcity = null;
      Chpilotcompstate = null;
      Chpilotcompstateabbr = null;
      Chpilotcomppostalcode = null;
      Chpilotcompcountry = null;
      Chpilotcompoffice = null;
      Chpilotcompfax = null;
      Chpilotcompmobile = null;

      // NON Aerodex fields
      Excbrokercompid = 0;
      Excbrokercomprelation = null;
      Excbrokercompagencytype = null;
      Excbrokercompbusinesstype = null;
      Excbrokercompname = null;
      Excbrokercompaltname = null;
      Excbrokercompemail = null;
      Excbrokercompwebaddress = null;
      Excbrokercompaddress1 = null;
      Excbrokercompaddress2 = null;
      Excbrokercompcity = null;
      Excbrokercompstate = null;
      Excbrokercompstateabbr = null;
      Excbrokercomppostalcode = null;
      Excbrokercompcountry = null;
      Excbrokercompoffice = null;
      Excbrokercompfax = null;
      Excbrokercompmobile = null;

      Acmfryear = 0;
      Acyear = 0;
      Acaportiatacode = null;
      Acaporticaocode = null;
      Acaportname = null;
      Acaportcity = null;
      Acaportstate = null;
      Acaportcountry = null;

      Acairframetothrs = 0;
      Acairframetotlandings = 0;
      Mxprog = null;
      Acpassengercount = 0;
      Acenginename = null;

      Acengine1serno = null;
      Acengine2serno = null;
      Acengine3serno = null;
      Acengine4serno = null;

      Acengine1tothrs = 0;
      Acengine2tothrs = 0;
      Acengine3tothrs = 0;
      Acengine4tothrs = 0;

      Acengine1sohhrs = 0;
      Acengine2sohhrs = 0;
      Acengine3sohhrs = 0;
      Acengine4sohhrs = 0;
      Acmainteohbyname = null;

      Acengine1shihrs = 0;
      Acengine2shihrs = 0;
      Acengine3shihrs = 0;
      Acengine4shihrs = 0;
      Acmainthotsbyname = null;

      Acengine1tbohrs = 0;
      Acengine2tbohrs = 0;
      Acengine3tbohrs = 0;
      Acengine4tbohrs = 0;

      Acavionicsavpackage = null;
      Acavionicsflightdir = null;
      Acavionicsautopilot = null;
      Acavionicsafis = null;
      Acavionicsfms = null;
      Acavionicsgps = null;
      Acavionicsins = null;
      Acavionicsirs = null;
      Acmaintained = null;

      Ifc1 = null;
      Ifc1yn = null;
      Ifc2 = null;
      Ifc2yn = null;
      Ifc3 = null;
      Ifc3yn = null;
      Ifc4 = null;
      Ifc4yn = null;
      Ifc5 = null;
      Ifc5yn = null;
      Ifc6 = null;
      Ifc6yn = null;

      Emp = null;
      Actimesasofdate = null;
      Acexteriormoyear = null;
      Acinteriormoyear = null;

      Aclifecyclestage = null;

      Infavorof = null;
      Docdate = null;
      Doctype = null;
      Docamount = null;

      Lessor = null;
      Amodweightclass = null;
      Amodjniqdescription = null;
      AcUsage = null;
    }

    [JsonPropertyName("snapshotdate")]
    public string? Snapyear_month { get; set; }

    [JsonPropertyName("year")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public int Snapyear { get; set; }

    [JsonPropertyName("month")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public int Snapmonth { get; set; }

    [JsonPropertyName("acid")]
    public long Acid { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    [JsonPropertyName("acjournid")]
    public long Acjournid { get; set; }

    [JsonPropertyName("airframetype")]
    public eAirFrameTypes Amodairframetypecode { get; set; }

    [JsonPropertyName("maketype")]
    public eMakeTypes Amodtypecode { get; set; }

    [JsonPropertyName("make")]
    public dynamic? Amodmakename { get; set; }

    [JsonPropertyName("model")]
    public dynamic? Amodmodelname { get; set; }

    [JsonPropertyName("modelicao")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string? Amodicaocode { get; set; }

    [JsonPropertyName("manufacturer")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string? Amodmanufacturer { get; set; }

    [JsonPropertyName("modelid")]
    public long Acmodelid { get; set; }

    [JsonPropertyName("serialnbr")]
    public dynamic? Acsernofull { get; set; }

    [JsonPropertyName("regnbr")]
    public dynamic? Acregno { get; set; }

    [JsonPropertyName("regexpiresdate")]
    public dynamic? Regexpiresdate { get; set; }

    // NON Aerodex fields
    [JsonPropertyName("status")]
    public dynamic? Acstatus { get; set; }

    [JsonPropertyName("asking")]
    public dynamic? Acasking { get; set; }

    [JsonPropertyName("askingamt")]
    public dynamic? Acaskingprice { get; set; }

    [JsonPropertyName("datelisted")]
    public dynamic? Aclistdate { get; set; }

    [JsonPropertyName("datepurchased")]
    public dynamic? Acpurchasedate { get; set; }

    [JsonPropertyName("datechanged")]
    public dynamic? Acupddate { get; set; }


    // comp 1 contact 1
    [JsonPropertyName("comp1id")]
    public long Comp1id { get; set; }

    [JsonPropertyName("comp1relation")]
    public dynamic? Comp1relation { get; set; }

    [JsonPropertyName("comp1agencytype")]
    public dynamic? Comp1agencytype { get; set; }

    [JsonPropertyName("comp1businesstype")]
    public dynamic? Comp1businesstype { get; set; }

    [JsonPropertyName("comp1name")]
    public dynamic? Comp1name { get; set; }

    [JsonPropertyName("comp1altname")]
    public dynamic? Comp1altname { get; set; }

    [JsonPropertyName("comp1email")]
    public dynamic? Comp1email { get; set; }

    [JsonPropertyName("comp1webaddress")]
    public dynamic? Comp1webaddress { get; set; }

    [JsonPropertyName("comp1address1")]
    public dynamic? Comp1address1 { get; set; }

    [JsonPropertyName("comp1address2")]
    public dynamic? Comp1address2 { get; set; }

    [JsonPropertyName("comp1city")]
    public dynamic? Comp1city { get; set; }

    [JsonPropertyName("comp1state")]
    public dynamic? Comp1state { get; set; }

    [JsonPropertyName("comp1stateabbr")]
    public dynamic? Comp1stateabbr { get; set; }

    [JsonPropertyName("comp1postalcode")]
    public dynamic? Comp1postalcode { get; set; }

    [JsonPropertyName("comp1country")]
    public dynamic? Comp1country { get; set; }

    [JsonPropertyName("comp1office")]
    public dynamic? Comp1office { get; set; }

    [JsonPropertyName("comp1fax")]
    public dynamic? Comp1fax { get; set; }

    [JsonPropertyName("comp1mobile")]
    public dynamic? Comp1mobile { get; set; }

    // comp 2 contatct 2
    [JsonPropertyName("comp2id")]
    public long Comp2id { get; set; }

    [JsonPropertyName("comp2relation")]
    public dynamic? Comp2relation { get; set; }

    [JsonPropertyName("comp2agencytype")]
    public dynamic? Comp2agencytype { get; set; }

    [JsonPropertyName("comp2businesstype")]
    public dynamic? Comp2businesstype { get; set; }

    [JsonPropertyName("comp2name")]
    public dynamic? Comp2name { get; set; }

    [JsonPropertyName("comp2altname")]
    public dynamic? Comp2altname { get; set; }

    [JsonPropertyName("comp2email")]
    public dynamic? Comp2email { get; set; }

    [JsonPropertyName("comp2webaddress")]
    public dynamic? Comp2webaddress { get; set; }

    [JsonPropertyName("comp2address1")]
    public dynamic? Comp2address1 { get; set; }

    [JsonPropertyName("comp2address2")]
    public dynamic? Comp2address2 { get; set; }

    [JsonPropertyName("comp2city")]
    public dynamic? Comp2city { get; set; }

    [JsonPropertyName("comp2state")]
    public dynamic? Comp2state { get; set; }

    [JsonPropertyName("comp2stateabbr")]
    public dynamic? Comp2stateabbr { get; set; }

    [JsonPropertyName("comp2postalcode")]
    public dynamic? Comp2postalcode { get; set; }

    [JsonPropertyName("comp2country")]
    public dynamic? Comp2country { get; set; }

    [JsonPropertyName("comp2office")]
    public dynamic? Comp2office { get; set; }

    [JsonPropertyName("comp2fax")]
    public dynamic? Comp2fax { get; set; }

    [JsonPropertyName("comp2mobile")]
    public dynamic? Comp2mobile { get; set; }

    // chief pilot comp and contact
    [JsonPropertyName("chpilotcompid")]
    public long Chpilotcompid { get; set; }

    [JsonPropertyName("chpilotcomprelation")]
    public dynamic? Chpilotcomprelation { get; set; }

    [JsonPropertyName("chpilotcompagencytype")]
    public dynamic? Chpilotcompagencytype { get; set; }

    [JsonPropertyName("chpilotcompbusinesstype")]
    public dynamic? Chpilotcompbusinesstype { get; set; }

    [JsonPropertyName("chpilotcompname")]
    public dynamic? Chpilotcompname { get; set; }

    [JsonPropertyName("chpilotcompaltname")]
    public dynamic? Chpilotcompaltname { get; set; }

    [JsonPropertyName("chpilotcompemail")]
    public dynamic? Chpilotcompemail { get; set; }

    [JsonPropertyName("chpilotcompwebaddress")]
    public dynamic? Chpilotcompwebaddress { get; set; }

    [JsonPropertyName("chpilotcompaddress1")]
    public dynamic? Chpilotcompaddress1 { get; set; }

    [JsonPropertyName("chpilotcompaddress2")]
    public dynamic? Chpilotcompaddress2 { get; set; }

    [JsonPropertyName("chpilotcompcity")]
    public dynamic? Chpilotcompcity { get; set; }

    [JsonPropertyName("chpilotcompstate")]
    public dynamic? Chpilotcompstate { get; set; }

    [JsonPropertyName("chpilotcompstateabbr")]
    public dynamic? Chpilotcompstateabbr { get; set; }

    [JsonPropertyName("chpilotcomppostalcode")]
    public dynamic? Chpilotcomppostalcode { get; set; }

    [JsonPropertyName("chpilotcompcountry")]
    public dynamic? Chpilotcompcountry { get; set; }

    [JsonPropertyName("chpilotcompoffice")]
    public dynamic? Chpilotcompoffice { get; set; }

    [JsonPropertyName("chpilotcompfax")]
    public dynamic? Chpilotcompfax { get; set; }

    [JsonPropertyName("chpilotcompmobile")]
    public dynamic? Chpilotcompmobile { get; set; }

    // NON Aerodex fields
    [JsonPropertyName("excbrokercompid")]
    public long Excbrokercompid { get; set; }

    [JsonPropertyName("excbrokercomprelation")]
    public dynamic? Excbrokercomprelation { get; set; }

    [JsonPropertyName("excbrokercompagencytype")]
    public dynamic? Excbrokercompagencytype { get; set; }

    [JsonPropertyName("excbrokercompbusinesstype")]
    public dynamic? Excbrokercompbusinesstype { get; set; }

    [JsonPropertyName("excbrokercompname")]
    public dynamic? Excbrokercompname { get; set; }

    [JsonPropertyName("excbrokercompaltname")]
    public dynamic? Excbrokercompaltname { get; set; }

    [JsonPropertyName("excbrokercompemail")]
    public dynamic? Excbrokercompemail { get; set; }

    [JsonPropertyName("excbrokercompwebaddress")]
    public dynamic? Excbrokercompwebaddress { get; set; }

    [JsonPropertyName("excbrokercompaddress1")]
    public dynamic? Excbrokercompaddress1 { get; set; }

    [JsonPropertyName("excbrokercompaddress2")]
    public dynamic? Excbrokercompaddress2 { get; set; }

    [JsonPropertyName("excbrokercompcity")]
    public dynamic? Excbrokercompcity { get; set; }

    [JsonPropertyName("excbrokercompstate")]
    public dynamic? Excbrokercompstate { get; set; }

    [JsonPropertyName("excbrokercompstateabbr")]
    public dynamic? Excbrokercompstateabbr { get; set; }

    [JsonPropertyName("excbrokercomppostalcode")]
    public dynamic? Excbrokercomppostalcode { get; set; }

    [JsonPropertyName("excbrokercompcountry")]
    public dynamic? Excbrokercompcountry { get; set; }

    [JsonPropertyName("excbrokercompoffice")]
    public dynamic? Excbrokercompoffice { get; set; }

    [JsonPropertyName("excbrokercompfax")]
    public dynamic? Excbrokercompfax { get; set; }

    [JsonPropertyName("excbrokercompmobile")]
    public dynamic? Excbrokercompmobile { get; set; }

    [JsonPropertyName("yearmfg")]
    public int Acmfryear { get; set; }

    [JsonPropertyName("yeardlv")]
    public int Acyear { get; set; }

    [JsonPropertyName("baseiata")]
    public dynamic? Acaportiatacode { get; set; }

    [JsonPropertyName("baseicao")]
    public dynamic? Acaporticaocode { get; set; }

    [JsonPropertyName("basename")]
    public dynamic? Acaportname { get; set; }

    [JsonPropertyName("basecity")]
    public dynamic? Acaportcity { get; set; }

    [JsonPropertyName("basestate")]
    public dynamic? Acaportstate { get; set; }

    [JsonPropertyName("basecountry")]
    public dynamic? Acaportcountry { get; set; }


    [JsonPropertyName("aftt")]
    public int Acairframetothrs { get; set; }

    [JsonPropertyName("landings")]
    public int Acairframetotlandings { get; set; }

    [JsonPropertyName("mxprog")]
    public dynamic? Mxprog { get; set; }

    [JsonPropertyName("passgr")]
    public int Acpassengercount { get; set; }

    [JsonPropertyName("engmodel")]
    public dynamic? Acenginename { get; set; }

    [JsonPropertyName("timesasofdate")]
    public dynamic? Actimesasofdate { get; set; }

    [JsonPropertyName("engsernbr1")]
    public dynamic? Acengine1serno { get; set; }

    [JsonPropertyName("engsernbr2")]
    public dynamic? Acengine2serno { get; set; }

    [JsonPropertyName("engsernbr3")]
    public dynamic? Acengine3serno { get; set; }

    [JsonPropertyName("engsernbr4")]
    public dynamic? Acengine4serno { get; set; }


    [JsonPropertyName("eng1tt")]
    public int Acengine1tothrs { get; set; }

    [JsonPropertyName("eng2tt")]
    public int Acengine2tothrs { get; set; }

    [JsonPropertyName("eng3tt")]
    public int Acengine3tothrs { get; set; }

    [JsonPropertyName("eng4tt")]
    public int Acengine4tothrs { get; set; }


    [JsonPropertyName("smoh1")]
    public int Acengine1sohhrs { get; set; }

    [JsonPropertyName("smoh2")]
    public int Acengine2sohhrs { get; set; }

    [JsonPropertyName("smoh3")]
    public int Acengine3sohhrs { get; set; }

    [JsonPropertyName("smoh4")]
    public int Acengine4sohhrs { get; set; }

    [JsonPropertyName("eohby")]
    public dynamic? Acmainteohbyname { get; set; }


    [JsonPropertyName("shot1")]
    public int Acengine1shihrs { get; set; }

    [JsonPropertyName("shot2")]
    public int Acengine2shihrs { get; set; }

    [JsonPropertyName("shot3")]
    public int Acengine3shihrs { get; set; }

    [JsonPropertyName("shot4")]
    public int Acengine4shihrs { get; set; }

    [JsonPropertyName("hotsby")]
    public dynamic? Acmainthotsbyname { get; set; }


    [JsonPropertyName("tbo1")]
    public int Acengine1tbohrs { get; set; }

    [JsonPropertyName("tbo2")]
    public int Acengine2tbohrs { get; set; }

    [JsonPropertyName("tbo3")]
    public int Acengine3tbohrs { get; set; }

    [JsonPropertyName("tbo4")]
    public int Acengine4tbohrs { get; set; }


    [JsonPropertyName("avionics")]
    public dynamic? Acavionicsavpackage { get; set; }

    [JsonPropertyName("fdir")]
    public dynamic? Acavionicsflightdir { get; set; }

    [JsonPropertyName("ap")]
    public dynamic? Acavionicsautopilot { get; set; }

    [JsonPropertyName("afis")]
    public dynamic? Acavionicsafis { get; set; }

    [JsonPropertyName("fms")]
    public dynamic? Acavionicsfms { get; set; }

    [JsonPropertyName("gps")]
    public dynamic? Acavionicsgps { get; set; }

    [JsonPropertyName("ins")]
    public dynamic? Acavionicsins { get; set; }

    [JsonPropertyName("irs")]
    public dynamic? Acavionicsirs { get; set; }

    [JsonPropertyName("maintained")]
    public dynamic? Acmaintained { get; set; }


    [JsonPropertyName("ifc1")]
    public dynamic? Ifc1 { get; set; }

    [JsonPropertyName("ifc1yn")]
    public dynamic? Ifc1yn { get; set; }

    [JsonPropertyName("ifc2")]
    public dynamic? Ifc2 { get; set; }

    [JsonPropertyName("ifc2yn")]
    public dynamic? Ifc2yn { get; set; }

    [JsonPropertyName("ifc3")]
    public dynamic? Ifc3 { get; set; }

    [JsonPropertyName("ifc3yn")]
    public dynamic? Ifc3yn { get; set; }

    [JsonPropertyName("ifc4")]
    public dynamic? Ifc4 { get; set; }

    [JsonPropertyName("ifc4yn")]
    public dynamic? Ifc4yn { get; set; }

    [JsonPropertyName("ifc5")]
    public dynamic? Ifc5 { get; set; }

    [JsonPropertyName("ifc5yn")]
    public dynamic? Ifc5yn { get; set; }

    [JsonPropertyName("ifc6")]
    public dynamic? Ifc6 { get; set; }

    [JsonPropertyName("ifc6yn")]
    public dynamic? Ifc6yn { get; set; }


    [JsonPropertyName("emp")]
    public dynamic? Emp { get; set; }

    [JsonPropertyName("extyr")]
    public dynamic? Acexteriormoyear { get; set; }

    [JsonPropertyName("intyr")]
    public dynamic? Acinteriormoyear { get; set; }


    [JsonPropertyName("lifecycle")]
    public dynamic? Aclifecyclestage { get; set; }

    [JsonPropertyName("infavorofcompid")]
    public long Infavorofcompid { get; set; }

    [JsonPropertyName("infavorof")]
    public dynamic? Infavorof { get; set; }

    [JsonPropertyName("docdate")]
    public dynamic? Docdate { get; set; }

    [JsonPropertyName("doctype")]
    public dynamic? Doctype { get; set; }

    [JsonPropertyName("docamount")]
    public dynamic? Docamount { get; set; }


    [JsonPropertyName("lessor")]
    public dynamic? Lessor { get; set; }

    [JsonPropertyName("weightclass")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public dynamic? Amodweightclass { get; set; }

    [JsonPropertyName("categorysize")]
    public dynamic? Amodjniqdescription { get; set; }

    [JsonPropertyName("acusage")]
    public dynamic? AcUsage { get; set; }
  }
  #endregion

  #region company_classes
  public class CompObjectListOptions
  {
    public CompObjectListOptions()
    {
      complist = null;
    }
    public List<int>? complist { get; set; }

  }
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
    public string entitytype { get; set; }
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
      entitytype = "";
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
  public class responseAllCompanyInfo
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int count { get; set; }
    public List<companyIdentClass>? allcompanyinfo { get; set; }
  }
  public class bulkCompanyRecordsClass
  {
    [JsonPropertyName("companyid")]
    public long comp_id { get; set; }

    [JsonPropertyName("aircraftid")]
    public long ac_id { get; set; }

    [JsonPropertyName("name")]
    public string? comp_name { get; set; }

    [JsonPropertyName("relationtype")]
    public string? actype_name { get; set; }

    [JsonPropertyName("businesstype")]
    public string? cbus_name { get; set; }

    [JsonPropertyName("address1")]
    public string? comp_address1 { get; set; }

    [JsonPropertyName("address2")]
    public string? comp_address2 { get; set; }

    [JsonPropertyName("city")]
    public string? comp_city { get; set; }

    [JsonPropertyName("state")]
    public string? comp_state { get; set; }

    [JsonPropertyName("statename")]
    public string? comp_state_name { get; set; }

    [JsonPropertyName("postcode")]
    public string? comp_postcode { get; set; }

    [JsonPropertyName("country")]
    public string? comp_country { get; set; }

    [JsonPropertyName("email")]
    public string? comp_email_address { get; set; }

    [JsonPropertyName("website")]
    public string? comp_web_address { get; set; }

    [JsonPropertyName("ownerpercent")]
    public dynamic? cref_owner_percent { get; set; }

    [JsonPropertyName("fractionpurchasedate")]
    public dynamic? cref_fraction_purchase_date { get; set; }

    [JsonPropertyName("fractionexpiresdate")]
    public dynamic? cref_fraction_expires_date { get; set; }

    public bulkCompanyRecordsClass()
    {

      comp_id = 0;
      ac_id = 0;
      comp_name = "";
      actype_name = "";
      cbus_name = "";
      comp_address1 = "";
      comp_address2 = "";
      comp_city = "";
      comp_state = "";
      comp_postcode = "";
      comp_country = "";
      comp_email_address = "";
      comp_web_address = "";
      cref_owner_percent = null;
      cref_fraction_purchase_date = null;
      cref_fraction_expires_date = null;
    }
  }
  public class companyReportClass
  {
    [JsonPropertyName("companyid")]
    public long comp_id { get; set; }

    [JsonPropertyName("parentcompanyid")]
    public long comp_parent_id { get; set; }

    [JsonPropertyName("name")]
    public dynamic? comp_name { get; set; }

    [JsonPropertyName("altname")]
    public dynamic? comp_name_alt { get; set; }

    [JsonPropertyName("address1")]
    public dynamic? comp_address1 { get; set; }

    [JsonPropertyName("address2")]
    public dynamic? comp_address2 { get; set; }

    [JsonPropertyName("city")]
    public dynamic? comp_city { get; set; }

    [JsonPropertyName("state")]
    public dynamic? comp_state { get; set; }

    [JsonPropertyName("stateabbr")]
    public dynamic? comp_state_name { get; set; }

    [JsonPropertyName("postcode")]
    public dynamic? comp_postcode { get; set; }

    [JsonPropertyName("country")]
    public dynamic? comp_country { get; set; }

    [JsonPropertyName("email")]
    public dynamic? comp_email_address { get; set; }

    [JsonPropertyName("website")]
    public dynamic? comp_web_address { get; set; }

    [JsonPropertyName("office")]
    public dynamic? comp_office { get; set; }

    [JsonPropertyName("fax")]
    public dynamic? comp_fax { get; set; }

    [JsonPropertyName("mobile")]
    public dynamic? comp_mobile { get; set; }

    public companyReportClass()
    {

      comp_id = 0;
      comp_name = null;
      comp_name_alt = null;
      comp_address1 = null;
      comp_address2 = null;
      comp_city = null;
      comp_state = null;
      comp_state_name = null;
      comp_postcode = null;
      comp_country = null;
      comp_email_address = null;
      comp_web_address = null;
      comp_office = null;
      comp_fax = null;
      comp_mobile = null;

    }
  }
  public class responseAllCompanyObjects
  {
    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int count { get; set; }
    public int currentpage { get; set; }
    public int maxpages { get; set; }
    public List<companyClass>? allcompanyobjects { get; set; }
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
      complist = null;
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
    public List<int>? complist { get; set; }

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
  public class bulkContactRecordsClass
  {
    [JsonPropertyName("aircraftid")]
    public long aircraft_id { get; set; }

    [JsonPropertyName("contactid")]
    public long contact_id { get; set; }

    [JsonPropertyName("companyid")]
    public long contact_comp_id { get; set; }

    [JsonPropertyName("sirname")]
    public dynamic? contact_sirname { get; set; }

    [JsonPropertyName("firstname")]
    public dynamic? contact_first_name { get; set; }

    [JsonPropertyName("middleinitial")]
    public dynamic? contact_middle_initial { get; set; }

    [JsonPropertyName("lastname")]
    public dynamic? contact_last_name { get; set; }

    [JsonPropertyName("suffix")]
    public dynamic? contact_suffix { get; set; }

    [JsonPropertyName("title")]
    public dynamic? contact_title { get; set; }

    [JsonPropertyName("email")]
    public dynamic? contact_email_address { get; set; }

    public bulkContactRecordsClass()
    {
      aircraft_id = 0;
      contact_id = 0;
      contact_comp_id = 0;
      contact_sirname = null;
      contact_first_name = null;
      contact_middle_initial = null;
      contact_last_name = null;
      contact_suffix = null;
      contact_title = null;
      contact_email_address = null;

    }
  }
  public class contactReportClass
  {

    [JsonPropertyName("contactid")]
    public long contact_id { get; set; }

    [JsonPropertyName("companyid")]
    public long contact_comp_id { get; set; }

    [JsonPropertyName("actiondate")]
    public dynamic? contact_action_date { get; set; }

    [JsonPropertyName("sirname")]
    public dynamic? contact_sirname { get; set; }

    [JsonPropertyName("firstname")]
    public dynamic? contact_first_name { get; set; }

    [JsonPropertyName("middleinitial")]
    public dynamic? contact_middle_initial { get; set; }

    [JsonPropertyName("lastname")]
    public dynamic? contact_last_name { get; set; }

    [JsonPropertyName("suffix")]
    public dynamic? contact_suffix { get; set; }

    [JsonPropertyName("title")]
    public dynamic? contact_title { get; set; }

    [JsonPropertyName("email")]
    public dynamic? contact_email_address { get; set; }

    [JsonPropertyName("office")]
    public dynamic? contact_office { get; set; }

    [JsonPropertyName("fax")]
    public dynamic? contact_fax { get; set; }

    [JsonPropertyName("mobile")]
    public dynamic? contact_mobile { get; set; }

    public contactReportClass()
    {

      contact_id = 0;
      contact_comp_id = 0;
      contact_action_date = null;
      contact_sirname = null;
      contact_first_name = null;
      contact_middle_initial = null;
      contact_last_name = null;
      contact_suffix = null;
      contact_title = null;
      contact_email_address = null;
      contact_office = null;
      contact_fax = null;
      contact_mobile = null;

    }
  }
  #endregion

  #region utility_classes
  public class bulkPhoneRecordsClass
  {
    [JsonPropertyName("companyid")]
    public long pnum_comp_id { get; set; }

    [JsonPropertyName("contactid")]
    public long pnum_contact_id { get; set; }

    [JsonPropertyName("phonetype")]
    public dynamic? pnum_type { get; set; }

    [JsonPropertyName("countrycode")]
    public dynamic? pnum_cntry_code { get; set; }

    [JsonPropertyName("areacode")]
    public dynamic? pnum_area_code { get; set; }

    [JsonPropertyName("prefix")]
    public dynamic? pnum_prefix { get; set; }

    [JsonPropertyName("number")]
    public dynamic? pnum_number { get; set; }

    [JsonPropertyName("phonenumber")]
    public dynamic? pnum_number_full { get; set; }

    public bulkPhoneRecordsClass()
    {
      pnum_contact_id = 0;
      pnum_comp_id = 0;
      pnum_type = null;
      pnum_cntry_code = null;
      pnum_area_code = null;
      pnum_prefix = null;
      pnum_number = null;
      pnum_number_full = null;

    }
  }
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

    [Description("New With Manufacturer")]
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
  public enum eModelTrendSummary : int
  {
    [Description("None")]
    [JsonPropertyName("N")]
    None = 0,

    [Description("Months")]
    [JsonPropertyName("M")]
    SumMonths = 1,

    [Description("Years")]
    [JsonPropertyName("Y")]
    SumYears = 2,

    [Description("Quarters")]
    [JsonPropertyName("Q")]
    SumQuarters = 3,

    [Description("Days")]
    [JsonPropertyName("D")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    SumDays = 4

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
  public class responseS3Sync
  {

    public string? responseid { get; set; }
    public string? responsestatus { get; set; }
    public int count { get; set; }
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
