using System;
using System.Collections.Generic;
using System.Threading;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using System.Collections;
using Gwit.Core.SyntaxHighlighting;
using System.IO;

namespace Gwit.SyntaxHighlighting.Pygments
{

    public class PygmentsHighlightingService : IHighlightingService
    {
        internal static System.Reflection.Assembly Assembly
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly(); }
        }

        internal static Version AssemblyVersion
        {
            get { return PygmentsHighlightingService.Assembly.GetName().Version; }
        }

        static ScriptEngine _engine;
        static ScriptSource _source;

        ScriptScope _scope;
        Thread _init_thread;

        private void InitializeHosting()
        {
            _engine = IronPython.Hosting.Python.CreateEngine();

            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            var stream = asm.GetManifestResourceStream("Gwit.SyntaxHighlighting.Pygments.PygmentsCodeSource.py");
            _source = _engine.CreateScriptSource(new BasicStreamContentProvider(stream), "PygmentsCodeSource.py");
        }

        public PygmentsHighlightingService()
        {
            if (_engine == null)
                InitializeHosting();

            _scope = _engine.CreateScope();

            _init_thread = new Thread(() => { _source.Execute(_scope); });
            _init_thread.Start();
        }

        string[] _styles;
        PygmentLanguage[] _lanugages;

        public PygmentLanguage[] Languages
        {
            get
            {
                if (_lanugages == null)
                {

                    _init_thread.Join();

                    var f = _scope.GetVariable<PythonFunction>("get_all_lexers");
                    var r = (IEnumerator)(PythonGenerator)_engine.Operations.Invoke(f);
                    var lanugages_list = new List<PygmentLanguage>();
                    while (r.MoveNext())
                    {
                        PythonTuple o = r.Current as PythonTuple;
                        lanugages_list.Add(new PygmentLanguage()
                            {
                                LongName = (string)o[0],
                                LookupName = (string)((PythonTuple)o[1])[0]
                            });
                    }

                    _lanugages = lanugages_list.ToArray();
                }
                return _lanugages;
            }
        }

        public string[] Styles
        {
            get
            {
                if (_styles == null)
                {

                    _init_thread.Join();

                    var f = _scope.GetVariable<PythonFunction>("get_all_styles");
                    var r = (IEnumerator)(PythonGenerator)_engine.Operations.Invoke(f);
                    var styles_list = new List<string>();
                    while (r.MoveNext())
                    {
                        styles_list.Add((string)r.Current);
                    }

                    _styles = styles_list.ToArray();

                }

                return _styles;
            }
        }

        Microsoft.Scripting.Utils.Func<object, object, object, string> _generatehtml_function;
        private string _lexerName;
        private string _options;

        Microsoft.Scripting.Utils.Func<object, object, object, string> _generate_html_for_file;

        public string GenerateHtml(string data, string fileName, object options)
        {
            if (_generatehtml_function == null)
            {
                _init_thread.Join();

                var f = _scope.GetVariable<PythonFunction>("generate_html_for_file");
                _generate_html_for_file = _engine.Operations.ConvertTo<Microsoft.Scripting.Utils.Func<object, object, object, string>>(f);
            }

            return _generate_html_for_file(data, fileName, null);
        }

    }
}
