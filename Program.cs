using jtcTestClient;
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


string accessToken = string.Empty;
string bearerToken = string.Empty;

const string TEST_API_BASE = "https://testcustomer.jetnetconnect.com/api/"; // test customer api
const string LIVE_API_BASE = "https://customer.jetnetconnect.com/api/"; // test customer api

string apiBase = ""; // base api path 

apiBase = LIVE_API_BASE; // TEST_API_BASE;

string authURL = "Admin/APILogin";
string aircraftControler = "Aircraft";
string companyControler = "Company";
string contactControler = "Contact";
string utilityControler = "Utility";

string getAccountInfo = utilityControler + "/getAccountInfo/{0}";

string getAircraft = aircraftControler + "/getAircraft/{0}/{1}";
string getAircraftList = aircraftControler + "/getAircraftList/{0}";
string getAircraftHistoryList = aircraftControler + "/getHistoryList/{0}";

string getCompany = companyControler + "/getCompany/{0}/{1}";
string getCompanyList = companyControler + "/getCompanyList/{0}";

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

// login to api example
responseAPILogin loginResponse = new();
apiConnection customerAPI = new();
string restURL = apiBase + authURL;
string returnValue = customerAPI.GetFromAPI("", restURL, loginUser, "PUT").Result;

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

// get aircraft example
//tmpString = string.Format(getAircraft, "214067", accessToken.Trim());
//restURL = apiBase + tmpString;
//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;
//responseAircraft aircraftResponse = new();

//if (returnValue is not null)
//{
//  aircraftResponse = JsonSerializer.Deserialize<responseAircraft>(returnValue, JsonSerializerOptions.Default)!;

//  if (aircraftResponse.responsestatus!.ToUpper().Contains(@"SUCCESS"))
//    Console.WriteLine("\ngetAircraft {0}", returnValue.Trim());
//  else
//    Console.WriteLine("\nERROR : getAircraft {0}", aircraftResponse.responsestatus!.Trim());

//}

//// get company example
//tmpString = string.Format(getCompany, "7223", accessToken.Trim());
//restURL = apiBase + tmpString;
//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;
//responseCompany companyResponse = new();

//if (returnValue is not null)
//{
//  companyResponse = JsonSerializer.Deserialize<responseCompany>(returnValue)!;

//  if (companyResponse.responsestatus!.ToUpper().Contains(@"SUCCESS"))
//    Console.WriteLine("\ngetCompany {0}", returnValue.Trim());
//  else
//    Console.WriteLine("\nERROR : getCompany {0}", companyResponse.responsestatus!.Trim());

//}

//// get contact example
//tmpString = string.Format(getContact, "345384", accessToken.Trim());
//restURL = apiBase + tmpString;
//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;
//responseContact contactResponse = new();

//if (returnValue is not null)
//{
//  contactResponse = JsonSerializer.Deserialize<responseContact>(returnValue)!;

//  if (contactResponse.responsestatus!.ToUpper().Contains(@"SUCCESS"))
//    Console.WriteLine("\ngetContact {0}", returnValue.Trim());
//  else
//    Console.WriteLine("\nERROR : getContact {0}", contactResponse.responsestatus!.Trim());

//}

// get aircraft list example

//tmpString = string.Format(getAircraftList, accessToken.Trim());
//restURL = apiBase + tmpString;

//List<string> tmpStateName = new();
//tmpStateName.Add("new york");
////tmpState.Add("NJ");
////tmpState.Add("PA");

//AcListOptions content = new()
//{
//  airframetype = eAirFrameTypes.None,
//  maketype = eMakeTypes.None,
//  sernbr = "",
//  regnbr = "",
//  modelid = 0,
//  make = "",
//  forsale = null,
//  lifecycle = eLifeCycle.InOperation,
//  basestate = null,
//  basestatename = tmpStateName,
//  basecountry = "United States",
//  basecode = "",
//  actiondate = "",
//  companyid = 0,
//  contactid = 0,
//  yearmfr = 0,
//  yeardlv = 0,
//  aircraftchanges = null,
//  aclist = null,
//  modlist = null
//};

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content, "PUT").Result;
//responseAircraftList aircraftList = new();

//if (returnValue is not null)
//{
//  aircraftList = JsonSerializer.Deserialize<responseAircraftList>(returnValue)!;

//  if (aircraftList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
//    Console.WriteLine("\naircraft list {0}", returnValue.Trim());
//  else
//    Console.WriteLine("\nERROR : aircraft list {0}", aircraftList.responsestatus!.Trim());

//}

//// get aircraft history list example

//tmpString = string.Format(getAircraftHistoryList, accessToken.Trim());
//restURL = apiBase + tmpString;

//AcHistoryOptions content3 = new()
//{
//  aircraftid = 0,
//  airframetype = eAirFrameTypes.None,
//  maketype = eMakeTypes.None,
//  modelid = 0,
//  make = "",
//  companyid = 0,
//  isnewaircraft = eYesNoIgnoreFlag.Ignore,
//  allrelationships = true,
//  transtype = null,
//  startdate = "",
//  enddate = "",
//  lastactionstartdate = "12-01-2008",
//  lastactionenddate = "12-31-2008",
//  aclist = null,
//  modlist = null
//};

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content3, "PUT").Result;
//responseAcHistory aircraftHistoryList = new();

//if (returnValue is not null)
//{
//  aircraftHistoryList = JsonSerializer.Deserialize<responseAcHistory>(returnValue)!;

//  if (aircraftHistoryList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
//    Console.WriteLine("\naircraft history list {0}", returnValue.Trim());
//  else
//    Console.WriteLine("\nERROR : aircraft history list {0}", aircraftHistoryList.responsestatus!.Trim());

//}

//// get copmpany list example

//tmpString = string.Format(getCompanyList, accessToken.Trim());
//restURL = apiBase + tmpString;

//List<string> tmpState = new();
//tmpState.Add("NY");
//tmpState.Add("NJ");
//tmpState.Add("PA");

//CompListOptions content1 = new()
//{
//  aircraftid = null,
//  name = "abc",
//  country = "",
//  city = "",
//  state = tmpState,
//  statename = null,
//  bustype = null,
//  airframetype = eAirFrameTypes.None,
//  maketype = eMakeTypes.None,
//  modelid = null,
//  make = null,
//  relationship = null,
//  isoperator = "",
//  actiondate = "",
//  companychanges = "false",
//  website = "",
//  complist = null
//};

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content1, "PUT").Result;
//responseCompanyList companyList = new();

//if (returnValue is not null)
//{
//  companyList = JsonSerializer.Deserialize<responseCompanyList>(returnValue)!;

//  if (companyList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
//    Console.WriteLine("\ncompany list {0}", returnValue.Trim());
//  else
//    Console.WriteLine("\nERROR : company list {0}", companyList.responsestatus!.Trim());

//}
//// get contact list example

//tmpString = string.Format(getContactList, accessToken.Trim());
//restURL = apiBase + tmpString;

//ContListOptions content2 = new()
//{
//  aircraftid = null,
//  companyid = 0,
//  companyname = "JETNET",
//  firstname = "",
//  lastname = "",
//  title = "",
//  email = "",
//  actiondate = "",
//  phonenumber = "",
//  contactchanges = "false",
//  contlist = null
//};

//returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content2, "PUT").Result;
//responseContactList contactList = new();

//if (returnValue is not null)
//{
//  contactList = JsonSerializer.Deserialize<responseContactList>(returnValue)!;

//  if (contactList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
//    Console.WriteLine("\ncontact list {0}", returnValue.Trim());
//  else
//    Console.WriteLine("\nERROR : contact list {0}", contactList.responsestatus!.Trim());
//}