using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace ResponseDataCollector.ServicesLibrary.Services;

public static class ResponseCertificate
{
    public static string GetCertificate(Uri uri)
    {
        RemoteCertificateValidationCallback certCallback = (_, _, _, _) => true;
        using var client = new TcpClient(uri.Host,uri.Port);
        using var sslStream = new SslStream(client.GetStream(), true, certCallback);
        sslStream.AuthenticateAsClientAsync(uri.Host);
        var serverCertificate = sslStream.RemoteCertificate;
        var certificate = new X509Certificate2(serverCertificate);

        return certificate.GetName();
    }
}
