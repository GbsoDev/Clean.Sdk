﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clean.Domain.Entity.Validations {
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
    public class EntityValidationMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal EntityValidationMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Clean.Domain.Entity.Validations.EntityValidationMessages", typeof(EntityValidationMessages).Assembly);
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
        ///   Looks up a localized string similar to The {0} entity is not valid to create.
        /// </summary>
        public static string InvalidEntityToCreate {
            get {
                return ResourceManager.GetString("InvalidEntityToCreate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} entity is not valid to update.
        /// </summary>
        public static string InvalidEntityToUpdate {
            get {
                return ResourceManager.GetString("InvalidEntityToUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} is required..
        /// </summary>
        public static string RequiredValidationError {
            get {
                return ResourceManager.GetString("RequiredValidationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} is invalid..
        /// </summary>
        public static string ValidateError {
            get {
                return ResourceManager.GetString("ValidateError", resourceCulture);
            }
        }
    }
}
