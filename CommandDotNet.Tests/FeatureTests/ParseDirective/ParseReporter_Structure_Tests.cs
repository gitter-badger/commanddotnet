using System.Threading.Tasks;
using CommandDotNet.TestTools.Scenarios;
using Xunit;
using Xunit.Abstractions;

namespace CommandDotNet.Tests.FeatureTests.ParseDirective
{
    public class ParseReporter_Structure_Tests 
    {
        public ParseReporter_Structure_Tests(ITestOutputHelper output)
        {
            Ambient.Output = output;
        }

        [Fact]
        public void Includes_Operands_Options_InheritedOptions_And_Operands_AreCalled_Arguments()
        {
            new AppRunner<App>()
                .UseParseDirective()
                .Verify(new Scenario
                {
                    When = {Args = "[parse] Do"},
                    Then =
                    {
                        Output = @"command: Do

arguments:

  operand1 <Text>
    value:
    inputs:
    default:

options:

  option1 <Text>
    value:
    inputs:
    default:

  iOption1 <Text>
    value:
    inputs:
    default:

Use [parse:t] to include token transformations.
"
                    }
                });
        }

        public class App
        {
            public Task<int> Interceptor(InterceptorExecutionDelegate next,
                [Option] string iOption1) => next();

            public void Do([Operand] string operand1, [Option] string option1)
            {

            }
        }
    }
}
