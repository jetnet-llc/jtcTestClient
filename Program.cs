using jtcTestClient;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

if (args.Length > 0)
{
  foreach (var arg in args)
  {
    Console.WriteLine($"Argument={arg}");
  }
}
else
{
  Console.WriteLine("No arguments");
}

Stopwatch timer = new Stopwatch();

string accessToken = string.Empty;
string bearerToken = string.Empty;

const string API_BASE = "https://customer.jetnetconnect.com/api/"; // live customer api
//const string API_BASE = "https://testcustomer.jetnetconnect.com/api/"; // test customer api

//const string API_BASE = "https://www.jetnet.connect.com/api/"; // local customer api (IIS)
//const string API_BASE = "https://localhost:44382/api/"; // local customer api (IISExpress)


string apiBase = ""; // base api path 

apiBase = API_BASE;

string authURL = "Admin/APILogin";
string aircraftControler = "Aircraft";
string companyControler = "Company";
string contactControler = "Contact";
string utilityControler = "Utility"; //CustomExports
string exportsControler = "CustomEndpoints";

string getAccountInfo = utilityControler + "/getAccountInfo/{0}";

string getAircraft = aircraftControler + "/getAircraft/{0}/{1}";
string getAircraftList = aircraftControler + "/getAircraftList/{0}";
string getAircraftHistoryList = aircraftControler + "/getHistoryList/{0}";
string getAircraftFlightsList = aircraftControler + "/getFlightData/{0}";
string getCondensedOwnerOperators = aircraftControler + "/getCondensedOwnerOperators/{0}";

string getGulfstreamExport = exportsControler + "/getGulfstreamExport/{0}";
string getFulerLinxExport = exportsControler + "/getFuelerLinxExport/{0}/{1}";

string getCompany = companyControler + "/getCompany/{0}/{1}";
string getCompanyList = companyControler + "/getCompanyList/{0}";

string getAllCompanyInfo = companyControler + "/getAllCompanyInfo/{0}";

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
  EmailAddress = @"mike@jetnet.com", // "demo@jetnet.com",
  Password = @"mmaggi7277" // "g846ii2v"
};

// login to api example
responseAPILogin loginResponse = new();
apiConnection customerAPI = new();
string restURL = apiBase + authURL;
string? returnValue = customerAPI.GetFromAPI("", restURL, loginUser, "PUT").Result;

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
string tmpString = string.Format(getAccountInfo, accessToken.Trim());
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

returnValue = null;

// get aircraft example
tmpString = string.Format(getAircraft, "214067", accessToken.Trim());
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

// get company example
tmpString = string.Format(getCompany, "7223", accessToken.Trim());
restURL = apiBase + tmpString;

// returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;

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
  aircraftid = 12798,
  startdate = "01/01/2000"
};

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content3, "PUT").Result;
responseAcHistory aircraftHistoryList = new();

if (returnValue is not null)
{
  aircraftHistoryList = JsonSerializer.Deserialize<responseAcHistory>(returnValue)!;

  if (aircraftHistoryList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\naircraft history list {0}", returnValue.Trim());
  else
    Console.WriteLine("\nERROR : aircraft history list {0}", aircraftHistoryList.responsestatus!.Trim());

}

returnValue = null;

// get Condensed Owner Operators report example

tmpString = string.Format(getCondensedOwnerOperators, accessToken.Trim());
restURL = apiBase + tmpString;

AcListOptions content5 = new()
{
  make = "GULFSTREAM",
  lifecycle = eLifeCycle.InOperation
};

timer.Start();
Console.WriteLine("\nSTART : {0} get Condensed Owner/Operator Report et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content5, "PUT").Result;
responseCondensedOwnerOperatorReport condensedOwnerOperatorReport = new();

if (returnValue is not null)
{
  condensedOwnerOperatorReport = JsonSerializer.Deserialize<responseCondensedOwnerOperatorReport>(returnValue, serializeOptions)!;

  if (condensedOwnerOperatorReport.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\nRETURN Condensed Owner/Operator Report {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
  else
    Console.WriteLine("\nERROR : Condensed Owner/Operator Report {0} et: {1:hh\\:mm\\:ss}", condensedOwnerOperatorReport.responsestatus!.Trim(), timer.Elapsed);

  Console.WriteLine("\nEND get Condensed Owner/Operator Report {0} et: {1:hh\\:mm\\:ss}", condensedOwnerOperatorReport.aircraftowneroperators!.Count.ToString(), timer.Elapsed);

}

timer.Stop();
Console.WriteLine("\nEND : {0} get Condensed Owner/Operator Report et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = null;

// get aircraft flights list example

tmpString = string.Format(getAircraftFlightsList, accessToken.Trim());
restURL = apiBase + tmpString;

AcFlightDataOptions content4 = new()
{
  aircraftid = 0,
  airframetype = eAirFrameTypes.None,
  sernbr = "",
  regnbr = "",
  maketype = eMakeTypes.None,
  modelid = 0,
  make = "",
  origin = "",
  destination = "",
  startdate = "08-01-2023", // 00:00:00",
  enddate = "08-02-2023", // 06:00:00",
  aclist = null,
  modlist = null,
  lastactionstartdate = "",
  lastactionenddate = ""
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

  Console.WriteLine("\nEND get aircraft flights list {0} et: {1:hh\\:mm\\:ss}", aircraftFlightsList.flightdata!.Count.ToString(), timer.Elapsed);

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

// get gulfstream export example

tmpString = string.Format(getFulerLinxExport, "N516MX", accessToken.Trim());
restURL = apiBase + tmpString;

timer.Reset();
timer.Start();

Console.WriteLine("\nSTART : {0} get FuelerLinx export et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;

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

tmpString = string.Format(getAllCompanyInfo, accessToken.Trim());
restURL = apiBase + tmpString;

timer.Reset();
timer.Start();

Console.WriteLine("\nSTART : {0} getAllCompanyInfo et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;
responseAllCompanyInfo allCompanyInfo = new();

if (returnValue is not null)
{
  allCompanyInfo = JsonSerializer.Deserialize<responseAllCompanyInfo>(returnValue)!;

  if (allCompanyInfo.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine("\nRETURN getAllCompanyInfo {0} et: {1:hh\\:mm\\:ss}", returnValue.Trim(), timer.Elapsed);
  else
    Console.WriteLine("\nERROR : getAllCompanyInfo {0} et: {1:hh\\:mm\\:ss}", allCompanyInfo.responsestatus!.Trim(), timer.Elapsed);

  Console.WriteLine("\nEND getAllCompanyInfo {0} et: {1:hh\\:mm\\:ss}", allCompanyInfo!.allcompanyinfo!.Count!.ToString(), timer.Elapsed);

}

timer.Stop();

Console.WriteLine("\nEND : {0} getAllCompanyInfo et: {1:hh\\:mm\\:ss}", DateTime.Now.ToLongTimeString(), timer.Elapsed);

returnValue = null;

// get copmpany list example

tmpString = string.Format(getCompanyList, accessToken.Trim());
restURL = apiBase + tmpString;

List<string> tmpState = new();
tmpState.Add("NY");
tmpState.Add("NJ");
tmpState.Add("PA");

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

// returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content1, "PUT").Result;
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
  contlist = null
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