# Sentinel - A simple integration tests executor for ASP.NET Applications

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
                    new DatabaseTest(
                        "Database connection Test", "Check database connection constantly", connectionString: "DefaultConnection"),
                    new MyCustomTest(TEST_NAME", "TEST_DESCRIPTION")
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

