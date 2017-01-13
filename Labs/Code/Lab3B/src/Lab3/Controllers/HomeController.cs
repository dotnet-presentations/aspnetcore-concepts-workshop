using Microsoft.AspNetCore.Mvc;

public class HomeController
{
    [HttpGet("/")]
    public string Index() => "Hello from MVC!";
}
