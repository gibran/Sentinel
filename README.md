# Sentinel - A simple integration test runner that runs periodically and displays the test results through a REST API

```C#
public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // ...
            app.UseSentinel(new SentinelOptions
            {
                TestResultStore = new InMemoryTestResultStore(), // default
                Tests = new List<SentinelTestBase>
                {
                    new MyCustomTest(
                        name: "My Custom Test Name",
                        description: "My Custom Test Description"),
                        
                    new DatabaseTest(
                        name: "Database connection Test", 
                        description: "Check database connection constantly", 
                        connectionString: "DefaultConnection",
                        interval:TimeSpan.FromSeconds(30))
                },
                OnTestResultChange = result =>
                {
                    Console.WriteLine(result.Name);
                }
            });
        }
    }
```

Run the sample app and access http://server:port/api/sentinel in order to retrieve the test results (json).

