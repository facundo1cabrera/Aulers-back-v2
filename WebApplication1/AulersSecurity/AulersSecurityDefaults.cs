namespace AulersApi.AulersSecurity
{
    public class AulersSecurityDefaults
    {
        public const string PUBLIC_KEYS_FOLDER_CONFIG_KEY = "AulersSecurity:PublicKeysFolder";
        public const string PUBLIC_KEYS_FOLDER_DEFAULT_CONFIG_VALUE = "public-keys";
        public const string PUBLIC_KEYS_FILENAME_CONFIG_KEY = @"AulersSecurity:PublicKeysFilenameRegex";
        public const string PUBLIC_KEYS_FILENAME_REGEX_DEFAULT_CONFIG_VALUE = "\\.xml$";
        public const string SUPERUSER_JWT_KEY = "isSU";
    }
}
