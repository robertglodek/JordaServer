﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Jorda.Server.Infrastructure.Common.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class IdentityGeneral {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal IdentityGeneral() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Jorda.Server.Infrastructure.Common.Resources.IdentityGeneral", typeof(IdentityGeneral).Assembly);
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
        ///   Looks up a localized string similar to Confirm Registration..
        /// </summary>
        public static string ConfirmRegistration {
            get {
                return ResourceManager.GetString("ConfirmRegistration", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Potwierdź swój email &lt;a href=&apos;{0}&apos;&gt;klikając tu&lt;/a&gt;..
        /// </summary>
        public static string ConfirmRegistrationEmail {
            get {
                return ResourceManager.GetString("ConfirmRegistrationEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email already confirmed.
        /// </summary>
        public static string EmailAlreadyConfirmed {
            get {
                return ResourceManager.GetString("EmailAlreadyConfirmed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email not nonfirmed.
        /// </summary>
        public static string EmailNotConfirmed {
            get {
                return ResourceManager.GetString("EmailNotConfirmed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error has occurred.
        /// </summary>
        public static string ErrorOccurred {
            get {
                return ResourceManager.GetString("ErrorOccurred", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid client token.
        /// </summary>
        public static string InvalidClientToken {
            get {
                return ResourceManager.GetString("InvalidClientToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password not valid..
        /// </summary>
        public static string PasswordNotValid {
            get {
                return ResourceManager.GetString("PasswordNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reset Password..
        /// </summary>
        public static string ResetPassword {
            get {
                return ResourceManager.GetString("ResetPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please reset your password by &lt;a href=&apos;{0}&apos;&gt;clicking here&lt;/a&gt;..
        /// </summary>
        public static string ResetPasswordEmail {
            get {
                return ResourceManager.GetString("ResetPasswordEmail", resourceCulture);
            }
        }
    }
}