using jtcTestClient;
using Newtonsoft.Json;
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

const string TEST_API_BASE = "https://testcustomer.jetnetconnect.com/api/"; // test sales api
const string LIVE_API_BASE = "https://customer.jetnetconnect.com/api/"; // test sales api
//const string LOCAL_API_BASE = "https://www.jetnetcustomer.com/api/"; // local utility api (IIS)
//const string LOCAL_API_BASE = "https://localhost:54501/api/"; // local customer api (IISExpress)

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

ApiUser loginUser = new ApiUser();
loginUser.EmailAddress = @"demo@jetnet.com";
loginUser.Password = @"g846ii2v";

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

// get copmpany list example

// get contact list example
