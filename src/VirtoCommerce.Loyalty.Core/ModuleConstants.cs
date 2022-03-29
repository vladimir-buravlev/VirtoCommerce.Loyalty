using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.Loyalty.Core
{
    public static class ModuleConstants
    {
        public static class Security
        {
            public static class Permissions
            {
                public const string Access = "Loyalty:access";
                public const string Create = "Loyalty:create";
                public const string Read = "Loyalty:read";
                public const string Update = "Loyalty:update";
                public const string Delete = "Loyalty:delete";

                public static string[] AllPermissions { get; } = { Read, Create, Access, Update, Delete };
            }
        }

        public static class Settings
        {
            public static class General
            {
                public static SettingDescriptor LoyaltyEnabled { get; } = new SettingDescriptor
                {
                    Name = "Loyalty.LoyaltyEnabled",
                    GroupName = "Loyalty|General",
                    ValueType = SettingValueType.Boolean,
                    DefaultValue = false
                };

                public static SettingDescriptor LoyaltyPassword { get; } = new SettingDescriptor
                {
                    Name = "Loyalty.LoyaltyPassword",
                    GroupName = "Loyalty|Advanced",
                    ValueType = SettingValueType.SecureString,
                    DefaultValue = "qwerty"
                };

                public static IEnumerable<SettingDescriptor> AllSettings
                {
                    get
                    {
                        yield return LoyaltyEnabled;
                        yield return LoyaltyPassword;
                    }
                }
            }

            public static IEnumerable<SettingDescriptor> AllSettings
            {
                get
                {
                    return General.AllSettings;
                }
            }
        }
    }
}
