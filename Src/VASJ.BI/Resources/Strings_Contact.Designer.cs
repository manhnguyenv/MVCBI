﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VASJ.BI.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Strings_Contact {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings_Contact() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("VASJ.BI.Resources.Strings_Contact", typeof(Strings_Contact).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;p&gt;Here is the latest contact form submission from {$Url}:&lt;/p&gt;
        ///&lt;table style=&quot;border-collapse:collapse;border-spacing:0;width:100%&quot;&gt;
        ///  &lt;tr&gt;
        ///    &lt;th style=&quot;border:1px #bbb solid;padding:10px;text-align:left;vertical-align:top;width:1%;white-space:nowrap;&quot;&gt;Full name:&lt;/th&gt;
        ///    &lt;td style=&quot;border:1px #bbb solid;padding:10px;vertical-align:top;&quot;&gt;{$fullName}&lt;/td&gt;
        ///  &lt;/tr&gt;
        ///  &lt;tr&gt;
        ///    &lt;th style=&quot;border:1px #bbb solid;padding:10px;text-align:left;vertical-align:top;width:1%;white-space:nowrap;&quot;&gt;Company name:&lt;/th [rest of string was truncated]&quot;;.
        /// </summary>
        public static string EmailBody {
            get {
                return ResourceManager.GetString("EmailBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Message from {$Url}.
        /// </summary>
        public static string EmailSubject {
            get {
                return ResourceManager.GetString("EmailSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to We&apos;d love to hear from you!.
        /// </summary>
        public static string FormTitle {
            get {
                return ResourceManager.GetString("FormTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;h2&gt;Thanks for your enquiry!&lt;/h2&gt;
        ///&lt;p&gt;Our team will be in touch shortly.&lt;/p&gt;.
        /// </summary>
        public static string Thanks {
            get {
                return ResourceManager.GetString("Thanks", resourceCulture);
            }
        }
    }
}
