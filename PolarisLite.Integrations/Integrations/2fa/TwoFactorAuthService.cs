using OtpNet;
using PolarisLite.Integrations.Settings;

namespace PolarisLite.Integrations.Integrations._2fa;
public static class TwoFactorAuthService
{
    private readonly static Totp totp;

    static TwoFactorAuthService()
    {
        byte[] secretKeyBytes = Base32Encoding.ToBytes(IntegrationSettings.TwoFASecret);
        totp = new Totp(secretKeyBytes);
    }

    public static string ComputetOtp()
    {
        return totp.ComputeTotp();
    }

    public static bool VerifyTotp(string totpCode)
    {
        return totp.VerifyTotp(totpCode, out long timeStepMatched);
    }
}