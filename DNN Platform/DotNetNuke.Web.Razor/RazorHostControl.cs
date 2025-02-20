﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information
namespace DotNetNuke.Web.Razor
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI;

    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Modules.Actions;
    using DotNetNuke.UI.Modules;

    [Obsolete("Deprecated in 9.3.2, will be removed in 11.0.0, use Razor Pages instead")]
    public class RazorHostControl : ModuleControlBase, IActionable
    {
        private readonly string razorScriptFile;

        private RazorEngine engine;

        /// <summary>Initializes a new instance of the <see cref="RazorHostControl"/> class.</summary>
        /// <param name="scriptFile">The path to the Razor script file.</param>
        [Obsolete("Deprecated in 9.3.2, will be removed in 11.0.0, use Razor Pages instead")]
        public RazorHostControl(string scriptFile)
        {
            this.razorScriptFile = scriptFile;
        }

        /// <inheritdoc/>
        [Obsolete("Deprecated in 9.3.2, will be removed in 11.0.0, use Razor Pages instead")]
        public ModuleActionCollection ModuleActions
        {
            get
            {
                if (this.Engine.Webpage is IActionable)
                {
                    return (this.Engine.Webpage as IActionable).ModuleActions;
                }

                return new ModuleActionCollection();
            }
        }

        [Obsolete("Deprecated in 9.3.2, will be removed in 11.0.0, use Razor Pages instead")]
        protected virtual string RazorScriptFile
        {
            get { return this.razorScriptFile; }
        }

        private RazorEngine Engine
        {
            get
            {
                if (this.engine == null)
                {
                    this.engine = new RazorEngine(this.RazorScriptFile, this.ModuleContext, this.LocalResourceFile);
                }

                return this.engine;
            }
        }

        /// <inheritdoc/>
        [Obsolete("Deprecated in 9.3.2, will be removed in 11.0.0, use Razor Pages instead")]
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!string.IsNullOrEmpty(this.RazorScriptFile))
            {
                var writer = new StringWriter();
                this.Engine.Render(writer);
                this.Controls.Add(new LiteralControl(HttpUtility.HtmlDecode(writer.ToString())));
            }
        }
    }
}
