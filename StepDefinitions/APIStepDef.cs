using System.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using SpecFlowBDDAutomationFramework.Models;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowBDDAutomationFramework.StepDefinitions;

[Binding]
public class APItStepDef
{
    private HttpClient _httpClient;
    private HttpResponseMessage _httpResponseMessage;
    private String responseBody;
    private readonly ISpecFlowOutputHelper _outputHelper;
    private readonly ScenarioContext _scenarioContext;

    public APItStepDef(ISpecFlowOutputHelper outputHelper, ScenarioContext scenarioContext)
    {
        _httpClient = new HttpClient();
        this._outputHelper = outputHelper;
        _scenarioContext = scenarioContext;
    }

    [Given(@"the user sends a request with url as ""(.*)""")]
    public async Task GivenTheUserSendsARequestWithUrlAs(string url)
    {
        _httpResponseMessage = await _httpClient.GetAsync(url);
        _httpResponseMessage.EnsureSuccessStatusCode();
        responseBody = await _httpResponseMessage.Content.ReadAsStringAsync();
        _outputHelper.WriteLine(responseBody);
        _scenarioContext["HttpResponse"] = responseBody;
        var desData = JsonConvert.DeserializeObject<datamodel>(responseBody);
    }

    [Then(@"request should be successful with status code as (.*)")]
    public void ThenRequestShouldBeSuccessfulWithStatusCodeAs(int statusCode)
    {
        Assert.True(_httpResponseMessage.IsSuccessStatusCode);
    }

    [Given(@"the user sends a post request with url as ""(.*)""")]
    public async Task GivenTheUserSendsAPostRequestWithUrlAs(string url)
    {
        Postdatamodel postdatamodel = new Postdatamodel()
        {
            name = "Priya",
            job = "Software Engineer"
        };
        string data = JsonConvert.SerializeObject(postdatamodel);
        var contentdata = new StringContent(data);
        _httpResponseMessage = await _httpClient.PostAsync(url, contentdata);
        responseBody = await _httpResponseMessage.Content.ReadAsStringAsync();
        _scenarioContext["HttpResponse"] = responseBody;
        _outputHelper.WriteLine("Post response is" + responseBody);
    }


    [Then(@"user should get a successul response")]
    public void ThenUserShouldGetASuccessulResponse()
    {
        Assert.IsTrue(_httpResponseMessage.IsSuccessStatusCode);
    }

    [Given(@"the user sends a put request with url as ""(.*)""")]
    public async Task GivenTheUserSendsAPutRequestWithUrlAs(string url)
    {
        Postdatamodel postdatamodel = new Postdatamodel()
        {
            name = "Priya",
            job = "Software Engineer Role"
        };
        string data = JsonConvert.SerializeObject(postdatamodel);
        var contentdata = new StringContent(data);
        _httpResponseMessage = await _httpClient.PutAsync(url, contentdata);
        responseBody = await _httpResponseMessage.Content.ReadAsStringAsync();
        _scenarioContext["HttpResponse"] = responseBody;
        _outputHelper.WriteLine("Post response is" + responseBody);
    }

    [Then(@"user should get a accepted response")]
    public void ThenUserShouldGetAAcceptedResponse()
    {
        Assert.IsTrue(_httpResponseMessage.StatusCode == HttpStatusCode.Accepted);
    }
}