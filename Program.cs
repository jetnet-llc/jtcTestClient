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

string getCompany = companyControler + "/getCompany/{0}/{1}";
string getCompanyList = companyControler + "/getCompanyList/{0}";

string getAllCompanyInfo = companyControler + "/getAllCompanyInfo/{0}/{1}/{2}";

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

// returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;

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
  modelid = 1271,
  make = "",
  forsale = "true",
  lifecycle = eLifeCycle.None,
  basestate = null,
  basestatename = null,
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

// returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content, "PUT").Result;
responseAircraftList aircraftList = new();

if (returnValue is not null)
{
  aircraftList = JsonSerializer.Deserialize<responseAircraftList>(returnValue)!;

  if (aircraftList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\naircraft list {0}", returnValue.Trim());
  else
    Console.WriteLine("\nERROR : aircraft list {0}", aircraftList.responsestatus!.Trim());

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

returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content5, "PUT").Result;
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
  startdate = "05/01/2025",
  //displayRange = 1
  //exactMatchReg = false
};

timer.Reset();
timer.Start();
Console.WriteLine("\nSTART : {0} get Condensed Snapshot et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content7, "PUT").Result;
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
  make = "GULFSTREAM", // ROBINSON GULFSTREAM KING AIR BOEING AIRBUS ASTRA
  //lifecycle = eLifeCycle.InOperation,
  //actiondate = "04/28/2025",
  //aircraftchanges = "true"
  //aclist = [121570]
};

timer.Reset();
timer.Start();

Console.WriteLine("\nSTART : {0} get Bulk Inport of Aircraft Records et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content6, "PUT").Result;
responseBulkExport bulkExportOutput = new();

if (returnValue is not null)
{
  try
  {
    bulkExportOutput = JsonSerializer.Deserialize<responseBulkExport>(returnValue, serializeOptions)!;

    if (bulkExportOutput.responsestatus!.ToUpper().Contains(@"SUCCESS"))
      Console.WriteLine("\nRETURN : Bulk Inport of Aircraft Records {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
    else
      Console.WriteLine("\nERROR : Bulk Inport of Aircraft Records {0} et: {1:hh\\:mm\\:ss}", bulkExportOutput.responsestatus!.Trim(), timer.Elapsed);

    Console.WriteLine("\nEND get Bulk Inport of Aircraft Records {0} et: {1:hh\\:mm\\:ss}", bulkExportOutput.exportaircraftcount!.ToString(), timer.Elapsed);
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

Console.WriteLine("\nEND : {0} get Bulk Inport of Aircraft Records et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = null;

// get aircraft flights list example

tmpString = string.Format(getAircraftFlightsList, accessToken.Trim());
restURL = apiBase + tmpString;

AcFlightDataOptions content4 = new()
{
  aircraftid = 0,
  airframetype = eAirFrameTypes.None,
  sernbr = "",
  regnbr = "N909XJ",
  maketype = eMakeTypes.None,
  modelid = 0,
  make = "",
  origin = "",
  destination = "",
  //startdate = "09-01-2024", // 00:00:00",
  //enddate = "09-02-2024", // 06:00:00",
  aclist = null,
  modlist = null,
  lastactionstartdate = "",
  lastactionenddate = ""
};

timer.Reset();
timer.Start();
Console.WriteLine("\nSTART : {0} get aircraft flights list et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

// returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content4, "PUT").Result;
responseAcFlightData aircraftFlightsList = new();

if (returnValue is not null)
{
  aircraftFlightsList = JsonSerializer.Deserialize<responseAcFlightData>(returnValue)!;

  if (aircraftFlightsList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\nRETURN aircraft flights list {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
  else
    Console.WriteLine("\nERROR : aircraft flights list {0} et: {1:hh\\:mm\\:ss}", aircraftFlightsList.responsestatus!.Trim(), timer.Elapsed);

  Console.WriteLine("\nEND get aircraft flights list et: {0:hh\\:mm\\:ss}", timer.Elapsed);

}

timer.Stop();

Console.WriteLine("\nEND : {0} get aircraft flights list et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

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

// returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content2, "PUT").Result;
responseContactList contactList = new();

if (returnValue is not null)
{
  contactList = JsonSerializer.Deserialize<responseContactList>(returnValue)!;

  if (contactList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\ncontact list {0}", returnValue.Trim());
  else
    Console.WriteLine("\nERROR : contact list {0}", contactList.responsestatus!.Trim());
}
