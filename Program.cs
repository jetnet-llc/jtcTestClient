using jtcTestClient;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Security.AccessControl;
using System;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

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

const string TEST_API_BASE = "https://testcustomer.jetnetconnect.com/api/"; // test sales api
const string LIVE_API_BASE = "https://customer.jetnetconnect.com/api/"; // test sales api

string apiBase = ""; // base api path 

apiBase = TEST_API_BASE;

string authURL = "Admin/APILogin";
string aircraftControler = "Aircraft";
string companyControler = "Company";
string contactControler = "Contact";
string utilityControler = "Utility";

string getAccountInfo = utilityControler + "/getAccountInfo/{0}";

string getAircraft = aircraftControler + "/getAircraft/{0}/{1}";
string getAircraftList = aircraftControler + "/getAircraftlist/{0}";

string getCompany = companyControler + "/getCompany/{0}/{1}";
string getCompanyList = companyControler + "/getCompanyList/{0}";

string getContact = contactControler + "/getContact/{0}/{1}";
string getContactList = contactControler + "/getContactList/{0}";

Console.WriteLine("Connect To API");

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
  loginResponse = JsonConvert.DeserializeObject<responseAPILogin>(returnValue)!;

  // bearer token and access token
  accessToken = loginResponse.ApiToken!;
  bearerToken = loginResponse.BearerToken!;

  Console.WriteLine(@"USER {0} customer API Token {1}", loginUser.EmailAddress.Trim(), accessToken.Trim());

}
else
  Console.WriteLine(@"ERROR : USER {0} FAILED LOGIN", loginUser.EmailAddress.Trim());

// get users account example
string tmpString = string.Format(getAccountInfo, accessToken.Trim());
restURL = apiBase + tmpString;
returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;
responseAccountInfo accountResponse = new();

if (returnValue is not null)
{
  accountResponse = JsonConvert.DeserializeObject<responseAccountInfo>(returnValue)!;

  if (accountResponse.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine(@"getAccountInfo {0}", returnValue.Trim());
  else
    Console.WriteLine(@"ERROR : getAccountInfo {0}", accountResponse.responsestatus!.Trim());

}

// get aircraft example
tmpString = string.Format(getAircraft, "202924", accessToken.Trim());
restURL = apiBase + tmpString;
returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;
responseAircraft aircraftResponse = new();

if (returnValue is not null)
{
  aircraftResponse = JsonConvert.DeserializeObject<responseAircraft>(returnValue)!;

  if (aircraftResponse.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine(@"getAircraft {0}", returnValue.Trim());
  else
    Console.WriteLine(@"ERROR : getAircraft {0}", aircraftResponse.responsestatus!.Trim());

}

// get company example
tmpString = string.Format(getCompany, "7223", accessToken.Trim());
restURL = apiBase + tmpString;
returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;
responseCompany companyResponse = new();

if (returnValue is not null)
{
  companyResponse = JsonConvert.DeserializeObject<responseCompany>(returnValue)!;

  if (companyResponse.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine(@"getCompany {0}", returnValue.Trim());
  else
    Console.WriteLine(@"ERROR : getCompany {0}", companyResponse.responsestatus!.Trim());

}

// get contact example
tmpString = string.Format(getContact, "345384", accessToken.Trim());
restURL = apiBase + tmpString;
returnValue = customerAPI.GetFromAPI(bearerToken, restURL, null).Result;
responseContact contactResponse = new();

if (returnValue is not null)
{
  contactResponse = JsonConvert.DeserializeObject<responseContact>(returnValue)!;

  if (contactResponse.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine(@"getContact {0}", returnValue.Trim());
  else
    Console.WriteLine(@"ERROR : getContact {0}", contactResponse.responsestatus!.Trim());

}

// get aircraft list example

tmpString = string.Format(getAircraftList, accessToken.Trim());
restURL = apiBase + tmpString;

AcListOptions content = new()
{
  airframetype = eAirFrameTypes.None,
  maketype = eMakeTypes.None,
  sernbr = "",
  regnbr = "",
  modelid = 0,
  makename = "ASTRA",
  isforsale = eYesNoIgnoreFlag.Ignore,
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
  aircraftchanges = false,
  aclist = null,
  modlist = null
};

returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content, "PUT").Result;
responseAircraftList aircraftList = new();

if (returnValue is not null)
{
  aircraftList = JsonConvert.DeserializeObject<responseAircraftList>(returnValue)!;

  if (aircraftList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine(@"aircraft list {0}", returnValue.Trim());
  else
    Console.WriteLine(@"ERROR : aircraft list {0}", aircraftList.responsestatus!.Trim());

}

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
  name = "",
  country = "",
  city = "",
  state = tmpState,
  statename = null,
  bustype = null,
  airframetype = eAirFrameTypes.None,
  maketype = eMakeTypes.None,
  modelid = null,
  makename = null,
  relationship = null,
  isoperator = eYesNoIgnoreFlag.Ignore,
  actiondate = "",
  companychanges = false,
  website = "",
  complist = null
};

returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content1, "PUT").Result;
responseCompanyList companyList = new();

if (returnValue is not null)
{
  companyList = JsonConvert.DeserializeObject<responseCompanyList>(returnValue)!;

  if (companyList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine(@"company list {0}", returnValue.Trim());
  else
    Console.WriteLine(@"ERROR : company list {0}", companyList.responsestatus!.Trim());

}
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
  contactchanges = false,
  contlist = null
};

returnValue = customerAPI.GetFromAPI(bearerToken, restURL, content2, "PUT").Result;
responseContactList contactList = new();

if (returnValue is not null)
{
  contactList = JsonConvert.DeserializeObject<responseContactList>(returnValue)!;

  if (contactList.responsestatus!.ToUpper().Contains(@"SUCCESS"))
    Console.WriteLine(@"contact list {0}", returnValue.Trim());
  else
    Console.WriteLine(@"ERROR : contact list {0}", contactList.responsestatus!.Trim());
}