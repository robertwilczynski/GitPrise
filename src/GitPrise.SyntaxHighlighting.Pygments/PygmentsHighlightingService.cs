#region License

// Copyright 2010 Robert Wilczynski (http://github.com/robertwilczynski/gitprise)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Collections.Generic;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using System.Collections;
using GitPrise.Core.SyntaxHighlighting;
using System.Reflection;
using ScriptUtils = Microsoft.Scripting.Utils;

namespace GitPrise.SyntaxHighlighting.Pygments
{

    public class PygmentsHighlightingService : IHighlightingService
    {
        internal static Assembly Assembly
        {
            get { return Assembly.GetExecutingAssembly(); }
        }

        internal static Version AssemblyVersion
        {
            get { return PygmentsHighlightingService.Assembly.GetName().Version; }
        }

        static ScriptEngine _engine;
        static ScriptSource _source;

        static ScriptScope _scope;

        private static void InitializeHosting()
        {
            _engine = IronPython.Hosting.Python.CreateEngine();

            Assembly asm = Assembly.GetExecutingAssembly();
            var stream = asm.GetManifestResourceStream("GitPrise.SyntaxHighlighting.Pygments.PygmentsCodeSource.py");
            _source = _engine.CreateScriptSource(new BasicStreamContentProvider(stream), "PygmentsCodeSource.py");
            _scope = _engine.CreateScope();

            _source.Execute(_scope);

        }

        static PygmentsHighlightingService()
        {
            InitializeHosting();
        }
         
        string[] _styles;
        PygmentLanguage[] _lanugages;

        public PygmentLanguage[] Languages
        {
            get
            {
                if (_lanugages == null)
                {

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

        ScriptUtils.Func<object, object, object, string> _generate_html_for_file;

        public string GenerateHtml(string data, string fileName, object options)
        {
            if (_generate_html_for_file == null)
            {

                var f = _scope.GetVariable<PythonFunction>("generate_html_for_file");
                _generate_html_for_file = _engine.Operations.ConvertTo<ScriptUtils.Func<object, object, object, string>>(f);
            }

            return _generate_html_for_file(data, fileName, null);
        }

    }
}
