// For useful output, start with: Discount -l 2 -e 1 discount.xml

namespace NxBRE.Examples
{
	using System;
	using System.Diagnostics;

	using net.ideaity.util;

	public class MainClass
	{
		public static bool SHOWSTACK = false;
		
		public static void Main(string[] args)
		{
			Arguments argOpt = new Arguments();
			
			SourceLevels engineTraceLevel = SourceLevels.Warning;
			SourceLevels ruleBaseTraceLevel = SourceLevels.Warning;
			
			argOpt.Usage = new string[]{"Usage",
																	"\tDiscount (options) [xmlfile]",
																	"",
																	"\toptions:",
																	"\t  -s | -S  Turn ON/OFF RuleContext dump",
																	"\t  -e [System.Diagnostics.SourceLevels]  Set the Engine Trace Source Level",
																	"\t  -l [System.Diagnostics.SourceLevels]  Set the RuleBase Trace Source Level",
																	"\t  -h       This message"};
			
			if (args.Length == 0) {
				argOpt.printUsage();
				System.Environment.Exit(1);
			}
			
			argOpt.parseArgumentTokens(args, new char[]{'e', 'l'});
						
			int c;
			while ((c = argOpt.getArguments()) != - 1)
			{
				switch (c)
				{
					
					case 's': 
						SHOWSTACK = true;
						break;
					
					case 'S': 
						SHOWSTACK = false;
						break;
					
					case 'e': 
						engineTraceLevel = (SourceLevels)Enum.Parse(typeof(SourceLevels), argOpt.StringParameter);
						break;
					
					case 'l': 
						ruleBaseTraceLevel = (SourceLevels)Enum.Parse(typeof(SourceLevels), argOpt.StringParameter);
						break;
					
					case 'h': 
						argOpt.printUsage();
						System.Environment.Exit(0);
						break;
					
					default: 
						break;
					
				}
			}
			
			string xmlFile = argOpt.getListFiles();
			CalculateTotal calculator = new CalculateTotal(xmlFile, engineTraceLevel, ruleBaseTraceLevel);
			
			if (calculator.IsValid) {
				Console.Out.WriteLine("\nOrder #1: Calculated discounted total={0} (expected: {1})\n",
				                      calculator.GetTotal(new Order(5, 25, "A")),
				                      25);
				
				Console.Out.WriteLine("\nOrder #2: Calculated discounted total={0} (expected: {1})\n",
				                      calculator.GetTotal(new Order(50, 250, "B")),
				                      225);
				
				Console.Out.WriteLine("\nOrder #3: Calculated discounted total={0} (expected: {1})\n",
				                      calculator.GetTotal(new Order(20, 200, "C")),
				                      160);
				
				Console.Out.WriteLine("\nOrder #4: Calculated discounted total={0} (expected: {1})\n",
				                      calculator.GetTotal(new Order(50, 500, "D")),
				                      350);
			}
		}
	}
}
