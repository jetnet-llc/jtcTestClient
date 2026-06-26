using jtcTestClient;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

bool bJustFileSync = false;

if (args.Length > 0)
{
  foreach (var arg in args)
  {
    Console.WriteLine($"Argument={arg}");
    if (arg.Length > 0 && arg.StartsWith("filesync"))
    {
      bJustFileSync = true;
    }
  }
}
else
{
  Console.WriteLine("No arguments");
}

// Non-interactive empty-IN() verification harness. Usage: jtcTestClient emptyintest [live|test]
if (args.Any(a => string.Equals(a, "emptyintest", StringComparison.OrdinalIgnoreCase)))
{
  string harnessEnv = args.Any(a => string.Equals(a, "test", StringComparison.OrdinalIgnoreCase)) ? "test" : "live";
  Environment.Exit(EmptyInHarness.RunAsync(harnessEnv).GetAwaiter().GetResult());
}

Stopwatch timer = new Stopwatch();

string accessToken = string.Empty;
string bearerToken = string.Empty;

//const string API_BASE = "https://customer.jetnetconnect.com/api/"; // live customer api
const string API_BASE = "https://testcustomer.jetnetconnect.com/api/"; // test customer api

//const string API_BASE = "https://10.100.16.129/api/"; // local customer api (IIS) 
//const string API_BASE = "https://localhost:44382/api/"; // local customer api (IISExpress)
//const string API_BASE = "https://localhost:5001/api/"; // local customer api (IIS)

string apiBase = ""; // base api path 

apiBase = API_BASE;

string authURL = "Admin/APILogin";
string aircraftControler = "Aircraft";
string companyControler = "Company";
string contactControler = "Contact";
string utilityControler = "Utility";
string exportsControler = "CustomEndpoints";

string getAccountInfo = utilityControler + "/getAccountInfo/{0}";

string getAircraft = aircraftControler + "/getAircraft/{0}/{1}";
string getAircraftPictures = aircraftControler + "/getPictures/{0}/{1}";
string getAircraftList = aircraftControler + "/getAircraftList/{0}";
string getAircraftHistoryList = aircraftControler + "/getHistoryList/{0}";
string getAircraftHistoryListPaged = aircraftControler + "/getHistoryListPaged/{0}/{1}/{2}";
string getAircraftFlightsList = aircraftControler + "/getFlightData/{0}";
string getCondensedOwnerOperators = aircraftControler + "/getCondensedOwnerOperators/{0}";
string getCondensedSnapshot = aircraftControler + "/getCondensedSnapshot/{0}";
string getBulkExport = aircraftControler + "/getBulkAircraftExport/{0}";

string getGulfstreamExport = exportsControler + "/getGulfstreamExport/{0}";
string getFulerLinxExport = exportsControler + "/getFuelerLinxExport/{0}/{1}";
string getNetjetsFlightList = exportsControler + "/getNetJetsFlightData/{0}";

string getCompany = companyControler + "/getCompany/{0}/{1}";
string getCompanyList = companyControler + "/getCompanyList/{0}";
string getCompanyContactList = companyControler + "/getCompanyContactList/{0}";

string getAllCompanyInfo = companyControler + "/getAllCompanyInfo/{0}/{1}/{2}";
string getAllCompanyObjects = companyControler + "/getAllCompanyObjects/{0}/{1}/{2}";

string getContact = contactControler + "/getContact/{0}/{1}";
string getContactList = contactControler + "/getContactList/{0}";

Console.WriteLine("Connect To API");

var serializeOptions = new JsonSerializerOptions
{
  WriteIndented = true,
  PropertyNameCaseInsensitive = true,
  Converters =
    {
        new JsonStringEnumConverter(null, true)
    }
};

ApiUser loginUser = new()
{
  EmailAddress = @"demo@jetnet.com",
  Password = @"g846ii2v"
};

apiConnection customerAPI = new();
string restURL = string.Empty;
string? returnValue = string.Empty;
responseAPILogin loginResponse = new();
string tmpString = string.Empty;


try
{
  // login to api example
  restURL = apiBase + authURL;
  returnValue = customerAPI.GetFromAPI("", restURL, loginUser, "PUT").Result;

  if (returnValue is not null)
  {
    loginResponse = JsonSerializer.Deserialize<responseAPILogin>(returnValue)!;

    // bearer token and access token
    accessToken = loginResponse.apiToken!;
    bearerToken = loginResponse.bearerToken!;

    Console.WriteLine("\nUSER {0} customer API Token {1}", loginUser.EmailAddress.Trim(), accessToken.Trim());

  }
  else
    Console.WriteLine("\nERROR : USER {0} FAILED LOGIN", loginUser.EmailAddress.Trim());

  returnValue = null;

  // get users account example
  tmpString = string.Format(getAccountInfo, accessToken.Trim());
  restURL = apiBase + tmpString;

  returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;

  responseAccountInfo accountResponse = new();

  if (returnValue is not null && !string.IsNullOrWhiteSpace(returnValue.Trim()))
  {
    accountResponse = JsonSerializer.Deserialize<responseAccountInfo>(returnValue)!;

    if (accountResponse.responsestatus!.ToUpper().Contains(@"SUCCESS"))
      Console.WriteLine("\ngetAccountInfo {0}", returnValue.Trim());
    else
      Console.WriteLine("\nERROR : getAccountInfo {0}", accountResponse.responsestatus!.Trim());

  }
  else
    Console.WriteLine("ERROR : getAccountInfo {0}", returnValue!.Trim());
}
catch (Exception ex)
{
  Console.WriteLine("returnValue : {0}", returnValue!.Trim());

  Console.WriteLine("ERROR : {0}", ex.Message!.Trim());
  var input = Console.ReadKey();
}


returnValue = null;

// get aircraft example
tmpString = string.Format(getAircraft, "52005", accessToken.Trim());
restURL = apiBase + tmpString;

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;
responseAircraft aircraftResponse = new();

if (returnValue is not null)
{
  aircraftResponse = JsonSerializer.Deserialize<responseAircraft>(returnValue, serializeOptions)!;

  if (aircraftResponse.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\ngetAircraft {0}", returnValue.Trim());
  else
    Console.WriteLine("\nERROR : getAircraft {0}", aircraftResponse.responsestatus!.Trim());

}

returnValue = null;

// get aircraft pictures example
tmpString = string.Format(getAircraftPictures, "52005", accessToken.Trim());
restURL = apiBase + tmpString;

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;
responseAcPictures responseAcPictures = new();

if (returnValue is not null)
{
  responseAcPictures = JsonSerializer.Deserialize<responseAcPictures>(returnValue, serializeOptions)!;

  if (responseAcPictures.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\ngetAircraftPictures {0}", returnValue.Trim());
  else
    Console.WriteLine("\nERROR : getAircraftPictures {0}", responseAcPictures.responsestatus!.Trim());

}

returnValue = null;

// get company example
tmpString = string.Format(getCompany, "7223", accessToken.Trim());
restURL = apiBase + tmpString;

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;
responseCompany companyResponse = new();

if (returnValue is not null)
{
  companyResponse = JsonSerializer.Deserialize<responseCompany>(returnValue)!;

  if (companyResponse.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\ngetCompany {0}", returnValue.Trim());
  else
    Console.WriteLine("\nERROR : getCompany {0}", companyResponse.responsestatus!.Trim());

}

returnValue = null;

// get contact example
tmpString = string.Format(getContact, "345384", accessToken.Trim());
restURL = apiBase + tmpString;

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;

responseContact contactResponse = new();

if (returnValue is not null)
{
  contactResponse = JsonSerializer.Deserialize<responseContact>(returnValue)!;

  if (contactResponse.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\ngetContact {0}", returnValue.Trim());
  else
    Console.WriteLine("\nERROR : getContact {0}", contactResponse.responsestatus!.Trim());

}

returnValue = null;

// get aircraft list example

tmpString = string.Format(getAircraftList, accessToken.Trim());
restURL = apiBase + tmpString;

List<string> tmpStateName = new();
tmpStateName.Add("new york");
//tmpState.Add("NJ");
//tmpState.Add("PA");

AcListOptions content = new()
{
  airframetype = eAirFrameTypes.None,
  maketype = eMakeTypes.None,
  sernbr = "",
  regnbr = "",
  modelid = 0,
  make = "",
  forsale = "",
  lifecycle = eLifeCycle.None,
  basestate = null,
  basestatename = tmpStateName,
  basecountry = "",
  basecode = "",
  actiondate = "",
  companyid = 0,
  contactid = 0,
  yearmfr = 0,
  yeardlv = 0,
  aircraftchanges = "",
  aclist = null,
  modlist = null
};

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content, "PUT").Result;
responseAircraftList aircraftList = new();

if (returnValue is not null)
{
  aircraftList = JsonSerializer.Deserialize<responseAircraftList>(returnValue)!;

  if (aircraftList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\naircraft list {0}", returnValue.Trim());
  else
    Console.WriteLine("\nERROR : aircraft list {0}", aircraftList.responsestatus!.Trim());

  //foreach (Aircraft2 ac in aircraftList.aircraft!)
  //{
  //  Console.WriteLine($"{ac.aircraftid},");// REG:{ac.regnbr} SER:{ac.sernbr} MFR:{ac.mfr} MODEL:{ac.model} MAKE:{ac.make}");

  //}

}

returnValue = null;

// get aircraft history list example

tmpString = string.Format(getAircraftHistoryList, accessToken.Trim());
restURL = apiBase + tmpString;

timer.Start();

Console.WriteLine("\nSTART : {0} get aircraft history list et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

List<eAcTransTypes> transtypes = new();
transtypes.Add(eAcTransTypes.FullSale);
transtypes.Add(eAcTransTypes.ShareSale);
transtypes.Add(eAcTransTypes.Lease);

//AcHistoryOptions content3 = new()
//{
//  aircraftid = 0,
//  airframetype = eAirFrameTypes.None,
//  maketype = eMakeTypes.None,
//  modelid = 0,
//  make = "GULFSTREAM",
//  companyid = 0,
//  isnewaircraft = eYesNoIgnoreFlag.Ignore,
//  allrelationships = true,
//  transtype = null, //transtypes,
//  startdate = "01/01/2024",
//  enddate = "",
//  lastactionstartdate = "",
//  lastactionenddate = "",
//  aclist = null,
//  modlist = null,
//  ispreownedtrans = eYesNoIgnoreFlag.Ignore,
//  isretailtrans = eYesNoIgnoreFlag.Ignore,
//  isinternaltrans = eYesNoIgnoreFlag.Ignore
//};

AcHistoryOptions content3 = new()
{
  //aircraftid = 207321
  //companyid = 4043
  //modelid = 28,
  make = "BELL",
  //allrelationships = true,
  startdate = "01/01/2025",
  enddate = "04/30/2025"
  //aclist = [52005, 246175, 200164, 126604]
};

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content3, "PUT").Result;
responseAcHistory aircraftHistoryList = new();

if (returnValue is not null)
{
  try
  {
    aircraftHistoryList = JsonSerializer.Deserialize<responseAcHistory>(returnValue)!;

    if (aircraftHistoryList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
      Console.WriteLine("\nRETURN : get aircraft history list {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
    else
      Console.WriteLine("\nERROR : get aircraft history list {0} et: {1:hh\\:mm\\:ss}", aircraftHistoryList.responsestatus!.Trim(), timer.Elapsed);

    Console.WriteLine("\nEND get aircraft history list {0} et: {1:hh\\:mm\\:ss}", aircraftHistoryList.count.ToString(), timer.Elapsed);
    timer.Stop();

  }
  catch (Exception ex)
  {
    timer.Stop();

    Console.WriteLine("returnValue : {0}", returnValue!.Trim());
    Console.WriteLine("ERROR : {0}", ex.Message!.Trim());
    var input = Console.ReadKey();
  }

}

Console.WriteLine("\nEND : {0} get aircraft history list et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

tmpString = string.Format(getAircraftHistoryListPaged, accessToken.Trim(), "1000", "1");
restURL = apiBase + tmpString;

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content3, "PUT").Result;
responseAcHistory aircraftHistoryList2 = new();

//if (returnValue is not null)
//{
//  aircraftHistoryList2 = JsonSerializer.Deserialize<responseAcHistory>(returnValue)!;

//  if (aircraftHistoryList2.responsestatus!.ToUpper().Contains(@"SUCCESS"))
//    Console.WriteLine("\naircraft history list {0}", returnValue.Trim());
//  else
//    Console.WriteLine("\nERROR : aircraft history list {0}", aircraftHistoryList2.responsestatus!.Trim());

//}

returnValue = null;

// get Condensed Owner Operators report example

tmpString = string.Format(getCondensedOwnerOperators, accessToken.Trim());
restURL = apiBase + tmpString;

AcListOptions content5 = new()
{
  make = "GULFSTREAM", // ROBINSON GULFSTREAM KING AIR BOEING AIRBUS ASTRA
  lifecycle = eLifeCycle.InOperation
};

timer.Reset();
timer.Start();
Console.WriteLine("\nSTART : {0} get Condensed Owner/Operator Report et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content5, "PUT").Result;
responseCondensedOwnerOperatorReport condensedOwnerOperatorReport = new();

if (returnValue is not null)
{
  try
  {
    condensedOwnerOperatorReport = JsonSerializer.Deserialize<responseCondensedOwnerOperatorReport>(returnValue, serializeOptions)!;

    if (condensedOwnerOperatorReport.responsestatus!.ToUpper().Contains(@"SUCCESS"))
      Console.WriteLine("\nRETURN : Condensed Owner/Operator Report {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
    else
      Console.WriteLine("\nERROR : Condensed Owner/Operator Report {0} et: {1:hh\\:mm\\:ss}", condensedOwnerOperatorReport.responsestatus!.Trim(), timer.Elapsed);

    Console.WriteLine("\nEND get Condensed Owner/Operator Report {0} et: {1:hh\\:mm\\:ss}", condensedOwnerOperatorReport.aircraftowneroperators!.Count.ToString(), timer.Elapsed);

    timer.Stop();

  }
  catch (Exception ex)
  {
    timer.Stop();

    Console.WriteLine("returnValue : {0}", returnValue!.Trim());
    Console.WriteLine("ERROR : {0}", ex.Message!.Trim());
    var input = Console.ReadKey();
  }
}

Console.WriteLine("\nEND : {0} get Condensed Owner/Operator Report et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = null;

Console.WriteLine("\nHit Any Key to Continue");
var inputCheck = Console.ReadKey();

// get Condensed Owner Operators Snapshot example
tmpString = string.Format(getCondensedSnapshot, accessToken.Trim());
restURL = apiBase + tmpString;

List<string> tmpRegNoList = new()
      {
        "N979KC", "HP-9921CMP", "N207BC", "N550VR", "PS-FSR", "N24ZD"
      };

AcSnapshotOptions content7 = new()
{
  //regnbrlist = tmpRegNoList
  //basecountry = "United Kingdom" 
  //aclist = [191409, 182956],
  //aclist = [204177, 191409, 182956],
  make = "GULFSTREAM", //"ROBINSON", ASTRA KING AIR GULFSTREAM
  //modelid = 278,
  //regnbr = "N266KW",
  //lifecycle = eLifeCycle.InOperation,
  startdate = "01/01/2026",
  //displayRange = 1
  //exactMatchReg = false
};

timer.Reset();
timer.Start();
Console.WriteLine("\nSTART : {0} get Condensed Snapshot et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content7, "PUT").Result;
responseCondensedSnapshot condensedSnapshot = new();

if (returnValue is not null)
{
  try
  {
    condensedSnapshot = JsonSerializer.Deserialize<responseCondensedSnapshot>(returnValue, serializeOptions)!;

    if (condensedSnapshot.responsestatus!.ToUpper().Contains(@"SUCCESS"))
      Console.WriteLine("\nRETURN : Condensed Snapshot {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
    else
      Console.WriteLine("\nERROR : Condensed Snapshot {0} et: {1:hh\\:mm\\:ss}", condensedSnapshot.responsestatus!.Trim(), timer.Elapsed);

    Console.WriteLine("\nEND get Condensed Snapshot {0} et: {1:hh\\:mm\\:ss}", condensedSnapshot.snapshotowneroperators!.Count.ToString(), timer.Elapsed);

    timer.Stop();

  }
  catch (Exception ex)
  {
    timer.Stop();

    Console.WriteLine("returnValue : {0}", returnValue!.Trim());
    Console.WriteLine("ERROR : {0}", ex.Message!.Trim());
    var input = Console.ReadKey();
  }
}

Console.WriteLine("\nEND : {0} get Condensed Snapshot et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = null;

Console.WriteLine("\nHit Any Key to Continue");
var inputCheck1 = Console.ReadKey();

// get bulk export example
tmpString = string.Format(getBulkExport, accessToken.Trim());
restURL = apiBase + tmpString;

AcListOptions content6 = new()
{
  make = "GULFSTREAM", // ROBINSON GULFSTREAM KING AIR BOEING AIRBUS ASTRA aclist = [227308, 29596],
  lifecycle = eLifeCycle.InOperation,
  //actiondate = "04/28/2025",
  //aircraftchanges = "true"
  //aclist = [227308, 29596, 232574],
  showHistoricalAcRefs = false,
  showAdditionalEquip = true,
  showExterior = true,
  showInterior = true,
  showMaintenance = true,
  showAvionics = true
};

timer.Reset();
timer.Start();

Console.WriteLine("\nSTART : {0} get Bulk Import of Aircraft Records et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content6, "PUT").Result;
responseBulkExport bulkExportOutput = new();

if (returnValue is not null)
{
  try
  {
    bulkExportOutput = JsonSerializer.Deserialize<responseBulkExport>(returnValue, serializeOptions)!;

    if (bulkExportOutput.responsestatus!.ToUpper().Contains(@"SUCCESS"))
      Console.WriteLine("\nRETURN : Bulk Import of Aircraft Records {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
    else
      Console.WriteLine("\nERROR : Bulk Import of Aircraft Records {0} et: {1:hh\\:mm\\:ss}", bulkExportOutput.responsestatus!.Trim(), timer.Elapsed);

    Console.WriteLine("\nEND get Bulk Import of Aircraft Records {0} et: {1:hh\\:mm\\:ss}", bulkExportOutput.exportaircraftcount!.ToString(), timer.Elapsed);
    Console.WriteLine("Bulk Import avionics records: {0}", bulkExportOutput.acavionicscount.ToString());
    timer.Stop();

  }
  catch (Exception ex)
  {
    timer.Stop();

    Console.WriteLine("returnValue : {0}", returnValue!.Trim());
    Console.WriteLine("ERROR : {0}", ex.Message!.Trim());
    var input = Console.ReadKey();
  }
}

Console.WriteLine("\nEND : {0} get Bulk Import of Aircraft Records et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = null;

// get aircraft flights list example

tmpString = string.Format(getAircraftFlightsList, accessToken.Trim());
restURL = apiBase + tmpString;

AcFlightDataOptions content4 = new()
{
  //aircraftid = 0,
  //airframetype = eAirFrameTypes.None,
  //sernbr = "",
  //regnbr = "N909XJ",
  maketype = eMakeTypes.BusinessJet,
  //modelid = 0,
  //make = "",
  //origin = "",
  //destination = "",
  startdate = "11/11/2025", // 00:00:00",
  enddate = "11/18/2025" // 06:00:00",
  //aclist = null,
  //modlist = null,
  //lastactionstartdate = "",
  //lastactionenddate = ""
};

timer.Reset();
timer.Start();
Console.WriteLine("\nSTART : {0} get aircraft flights list et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content4, "PUT").Result;
responseAcFlightData aircraftFlightsList = new();

if (returnValue is not null)
{
  aircraftFlightsList = JsonSerializer.Deserialize<responseAcFlightData>(returnValue)!;

  if (aircraftFlightsList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\nRETURN aircraft flights list {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
  else
    Console.WriteLine("\nERROR : aircraft flights list {0} et: {1:hh\\:mm\\:ss}", aircraftFlightsList.responsestatus!.Trim(), timer.Elapsed);

  Console.WriteLine("\nEND get aircraft flights {0} list et: {1:hh\\:mm\\:ss}", aircraftFlightsList.count.ToString(), timer.Elapsed);

}

timer.Stop();

Console.WriteLine("\nEND : {0} get aircraft flights list et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = null;

Console.WriteLine("\nHit Any Key to Continue");
var inputCheck3 = Console.ReadKey();

// get aircraft flights list example

tmpString = string.Format(getNetjetsFlightList, accessToken.Trim());
restURL = apiBase + tmpString;

AcFlightDataOptions content9 = new()
{
  //aircraftid = 0,
  //airframetype = eAirFrameTypes.None,
  //maketype = eMakeTypes.BusinessJet,
  //sernbr = "",
  //regnbr = "N90CE",
  //modelid = 0,
  //make = "",
  //origin = "",
  //destination = "",
  startdate = "11/11/2025", // 00:00:00",
  enddate = "11/18/2025" // 06:00:00",
};

timer.Reset();
timer.Start();
Console.WriteLine("\nSTART : {0} get aircraft netjet flights list et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content9, "PUT").Result;

responseAcFlightData aircraftNetjetFlightsList = new();

if (returnValue is not null)
{
  aircraftNetjetFlightsList = JsonSerializer.Deserialize<responseAcFlightData>(returnValue)!;

  if (aircraftNetjetFlightsList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\nRETURN aircraft netjet flights list {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
  else
    Console.WriteLine("\nERROR : aircraft netjet flights list {0} et: {1:hh\\:mm\\:ss}", aircraftNetjetFlightsList.responsestatus!.Trim(), timer.Elapsed);

  Console.WriteLine("\nEND get aircraft netjet flights {0} list et: {0:hh\\:mm\\:ss}", aircraftNetjetFlightsList.count.ToString(), timer.Elapsed);

}

timer.Stop();

Console.WriteLine("\nEND : {0} get aircraft netjet flights list et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = null;
// get gulfstream export example

tmpString = string.Format(getGulfstreamExport, accessToken.Trim());
restURL = apiBase + tmpString;

timer.Reset();
timer.Start();

Console.WriteLine("\nSTART : {0} get gulfstream export et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;

Console.WriteLine("\nRESPONSE : {0} et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

responseGulfstreamExport gulfstreamExport = new();

if (returnValue is not null)
{
  gulfstreamExport = JsonSerializer.Deserialize<responseGulfstreamExport>(returnValue, serializeOptions)!;

  if (gulfstreamExport.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\nRETURN get gulfstream export {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
  else
    Console.WriteLine("\nERROR : get gulfstream export {0} et: {1:hh\\:mm\\:ss}", gulfstreamExport.responsestatus!.Trim(), timer.Elapsed);

  Console.WriteLine("\nEND get gulfstream export {0} et: {1:hh\\:mm\\:ss}", gulfstreamExport.exportaircraftcount!.ToString(), timer.Elapsed);

}

timer.Stop();

Console.WriteLine("\nEND : {0} get gulfstream export et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = null;

// get Fuler Linx Export example

tmpString = string.Format(getFulerLinxExport, "N516MX", accessToken.Trim());
restURL = apiBase + tmpString;

timer.Reset();
timer.Start();

Console.WriteLine("\nSTART : {0} get FuelerLinx export et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;

Console.WriteLine("\nRESPONSE : {0} et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

responseFuelerLinxExport fuelerLinxExport = new();

if (returnValue is not null)
{
  fuelerLinxExport = JsonSerializer.Deserialize<responseFuelerLinxExport>(returnValue, serializeOptions)!;

  if (fuelerLinxExport.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\nRETURN get FuelerLinx export {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
  else
    Console.WriteLine("\nERROR : get FuelerLinx export {0} et: {1:hh\\:mm\\:ss}", fuelerLinxExport.responsestatus!.Trim(), timer.Elapsed);

  Console.WriteLine("\nEND get FuelerLinx export {0} et: {1:hh\\:mm\\:ss}", "1", timer.Elapsed);

}

timer.Stop();

Console.WriteLine("\nEND : {0} get FuelerLinx export et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = null;
// get all company info example

tmpString = string.Format(getAllCompanyInfo, accessToken.Trim(), "1000", "1");
restURL = apiBase + tmpString;

timer.Reset();
timer.Start();

Console.WriteLine("\nSTART : {0} getAllCompanyInfo PAGED et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;
responseAllCompanyInfo allCompanyInfo = new();

if (returnValue is not null)
{
  allCompanyInfo = JsonSerializer.Deserialize<responseAllCompanyInfo>(returnValue)!;

  if (allCompanyInfo.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\nRETURN getAllCompanyInfo PAGED {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
  else
    Console.WriteLine("\nERROR : getAllCompanyInfo PAGED {0} et: {1:hh\\:mm\\:ss}", allCompanyInfo.responsestatus!.Trim(), timer.Elapsed);

  Console.WriteLine("\nEND getAllCompanyInfo PAGED {0} et: {1:hh\\:mm\\:ss}", allCompanyInfo!.allcompanyinfo!.Count!.ToString(), timer.Elapsed);

}

timer.Stop();

Console.WriteLine("\nEND : {0} getAllCompanyInfo PAGED et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = null;
// get all company objects example

tmpString = string.Format(getAllCompanyObjects, accessToken.Trim(), "0", "0");
restURL = apiBase + tmpString;

timer.Reset();
timer.Start();

CompObjectListOptions content8 = new()
{
  complist = [6328, 9951, 14995, 18535, 23044, 28345, 30149, 32027, 32331, 32584,
              68826, 141335, 144424, 144652, 148776, 148954, 149480, 153776, 163302, 166522,
              217885, 219048, 226588, 228462, 229999, 230939, 236369, 236742, 251526, 251903,
              257851, 264126, 265939, 267113, 278427, 280145, 284384, 284416, 284844, 285491,
              285812, 287517, 290950, 303592, 308590, 310432, 316926, 324954, 325445, 326054,
              331563, 333669, 336507, 336554, 338057, 338069, 339483, 339564, 340274, 341576,
              345995, 346052, 347965, 349702, 350537, 351104, 351842, 356892, 358910, 359695,
              361069, 361241, 362581, 362748, 362911, 363572, 365157, 368523, 372506, 373442,
              374522, 374843, 375209, 375428, 376214, 376214, 377261, 377506, 377736, 377737,
              380546, 381200, 384732, 384813, 386678, 387195, 387465, 387683, 389468, 390223,
              391096, 392610, 392797, 393325, 393350, 398700, 399472, 402505, 402915, 403363,
              403451, 404072, 404519, 405309, 406023, 406526, 406886, 407144, 407937, 408771,
              410902, 410925, 412438, 413515, 415099, 415230, 415393, 416193, 416693, 417339,
              418332, 418686, 420271, 421772, 421880, 422229, 422660, 424230, 424986, 425644,
              426337, 426376, 428639, 429591, 431602, 432404, 432471, 432472, 432936, 433190,
              433377, 434685, 435415, 439841, 440011, 440715, 442122, 442462, 442563, 443679,
              444023, 445360, 446176, 447200, 447418, 447617, 447769, 448720, 448794, 449182,
              449253, 449843, 450779, 450898, 452104, 452935, 453220, 453344, 453994, 454755,
              455130, 455176, 455319, 455323, 456902, 456961, 458022, 458941, 459105, 459656,
              459838, 460401, 461077, 461174, 461176, 461178, 461622, 461625, 461796, 461865,
              462538, 462725, 462918, 463006, 463761, 464513, 466274, 466363, 466863, 467434,
              467537, 467549, 467579, 467603, 467790, 468192, 468762, 468797, 468817, 469052,
              469226, 469577, 469778, 470383, 470954, 471053, 471121, 471282, 471288, 471535,
              471579, 472348, 472928, 473116, 473217, 473284, 473918, 474505, 474843, 475274,
              475911, 476131, 476173, 476605, 477432, 478112, 478293, 478394, 478650, 479363,
              481033, 481313, 481856, 482409, 482826, 483112, 484087, 484382, 484960, 485158,
              485498, 486273, 486300, 487007, 487348, 487551, 489148, 489171, 489642, 490197,
              490368, 491492, 491566, 493966, 495147, 498048, 498049, 501545, 501982, 503310,
              503322, 504560, 504765, 505271, 506833, 507930, 508540, 509439, 510645, 511423,
              512244, 512375, 513151, 513290, 513372, 513638]
};

Console.WriteLine("\nSTART : {0} getAllCompanyObjects et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content8, "PUT").Result;
responseAllCompanyObjects allCompanyObjects = new();

if (returnValue is not null)
{
  allCompanyObjects = JsonSerializer.Deserialize<responseAllCompanyObjects>(returnValue)!;

  if (allCompanyObjects.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\nRETURN getAllCompanyObjects {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
  else
    Console.WriteLine("\nERROR : getAllCompanyObjects {0} et: {1:hh\\:mm\\:ss}", allCompanyObjects.responsestatus!.Trim(), timer.Elapsed);

  Console.WriteLine("\nEND getAllCompanyObjects {0} et: {1:hh\\:mm\\:ss}", allCompanyObjects!.allcompanyobjects!.Count!.ToString(), timer.Elapsed);

}

timer.Stop();

Console.WriteLine("\nEND : {0} getAllCompanyObjects et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

Console.WriteLine("\nHit Any Key to Continue");
var inputCheck2 = Console.ReadKey();

returnValue = null;
// get all company objects paged example

tmpString = string.Format(getAllCompanyObjects, accessToken.Trim(), "1", "1000");
restURL = apiBase + tmpString;

timer.Reset();
timer.Start();

Console.WriteLine("\nSTART : {0} getAllCompanyObjects PAGED et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content8, "PUT").Result;
responseAllCompanyObjects allCompanyObjectsPgd = new();

if (returnValue is not null)
{
  allCompanyObjectsPgd = JsonSerializer.Deserialize<responseAllCompanyObjects>(returnValue)!;

  if (allCompanyObjectsPgd.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\nRETURN getAllCompanyObjects PAGED {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
  else
    Console.WriteLine("\nERROR : getAllCompanyObjects PAGED {0} et: {1:hh\\:mm\\:ss}", allCompanyObjectsPgd.responsestatus!.Trim(), timer.Elapsed);

  Console.WriteLine("\nEND getAllCompanyObjects PAGED {0} et: {1:hh\\:mm\\:ss}", allCompanyObjectsPgd!.allcompanyobjects!.Count!.ToString(), timer.Elapsed);

}

timer.Stop();

Console.WriteLine("\nEND : {0} getAllCompanyObjects PAGED et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = null;
// get copmpany list example 

tmpString = string.Format(getCompanyList, accessToken.Trim());
restURL = apiBase + tmpString;

List<string> tmpState = new();
tmpState.Add("NY");
tmpState.Add("NJ");
tmpState.Add("PA");
tmpState.Add("MA");
tmpState.Add("VT");
tmpState.Add("RI");
tmpState.Add("ME");

CompListOptions content1 = new()
{
  aircraftid = null,
  name = "ab",
  country = "",
  city = "",
  state = tmpState,
  statename = null,
  bustype = null,
  airframetype = eAirFrameTypes.None,
  maketype = eMakeTypes.None,
  modelid = null,
  make = null,
  relationship = null,
  isoperator = "",
  actiondate = "",
  companychanges = "false",
  website = "",
  complist = null
};

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content1, "PUT").Result;
responseCompanyList companyList = new();

if (returnValue is not null)
{
  companyList = JsonSerializer.Deserialize<responseCompanyList>(returnValue)!;

  if (companyList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\ncompany list {0}", returnValue.Trim());
  else
    Console.WriteLine("\nERROR : company list {0}", companyList.responsestatus!.Trim());

}

returnValue = null;

// get contact list example

tmpString = string.Format(getContactList, accessToken.Trim());
restURL = apiBase + tmpString;

ContListOptions content2 = new()
{
  aircraftid = null,
  companyid = 0,
  companyname = "JETNET",
  firstname = "",
  lastname = "",
  title = "",
  email = "",
  actiondate = "",
  phonenumber = "",
  contactchanges = "false",
  contlist = null,
  complist = null
};

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content2, "PUT").Result;
responseContactList contactList = new();

if (returnValue is not null)
{
  contactList = JsonSerializer.Deserialize<responseContactList>(returnValue)!;

  if (contactList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\ncontact list {0}", returnValue.Trim());
  else
    Console.WriteLine("\nERROR : contact list {0}", contactList.responsestatus!.Trim());
}

returnValue = null;

// get company contact list example

tmpString = string.Format(getCompanyContactList, accessToken.Trim());
restURL = apiBase + tmpString;

List<string> tmpCcState = new();
tmpCcState.Add("NY");
tmpCcState.Add("NJ");

CompContactListOptions content10 = new()
{
  name = "ab",
  country = "",
  city = "",
  //state = tmpCcState,
  statename = null,
  bustype = null,
  relationship = null,
  website = "",
  postalcode = "",
  complist = null,
  actiondate = "",
  showcontacts = true
};

timer.Reset();
timer.Start();
Console.WriteLine("\nSTART : {0} get company contact list et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content10, "PUT").Result;
responseCompanyContactList companyContactList = new();

if (returnValue is not null)
{
  try
  {
    companyContactList = JsonSerializer.Deserialize<responseCompanyContactList>(returnValue, serializeOptions)!;

    if (companyContactList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
      Console.WriteLine("\nRETURN : company contact list {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
    else
      Console.WriteLine("\nERROR : company contact list {0} et: {1:hh\\:mm\\:ss}", companyContactList.responsestatus!.Trim(), timer.Elapsed);

    Console.WriteLine("\nEND get company contact list {0} et: {1:hh\\:mm\\:ss}", companyContactList.count.ToString(), timer.Elapsed);
    timer.Stop();
  }
  catch (Exception ex)
  {
    timer.Stop();

    Console.WriteLine("returnValue : {0}", returnValue!.Trim());
    Console.WriteLine("ERROR : {0}", ex.Message!.Trim());
    var input = Console.ReadKey();
  }
}

Console.WriteLine("\nEND : {0} get company contact list et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);
