using NReco.PhantomJS;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var phantomJS = new PhantomJS();

            // write result to stdout
            Console.WriteLine("Getting content from baidu.com directly to C# code...");
            var outFileHtml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "baidu.html");
            if (File.Exists(outFileHtml))
                File.Delete(outFileHtml);
            using (var outFs = new FileStream(outFileHtml, FileMode.OpenOrCreate, FileAccess.Write))
            {
                try
                {
                    phantomJS.RunScript(@"
						var system = require('system');
						var page = require('webpage').create();
						page.open('https://www.baidu.com/s?wd=site%3A51sole.com', function() {
							system.stdout.writeLine(page.content);
							phantom.exit();
						});
					", null, null, outFs);//or page.render('baidu.png');
                }
                finally
                {
                    phantomJS.Abort(); // ensure that phantomjs.exe is stopped
                }
            }
            Console.WriteLine("Result is saved into " + outFileHtml);
            Console.Read();
        }

    }
}
