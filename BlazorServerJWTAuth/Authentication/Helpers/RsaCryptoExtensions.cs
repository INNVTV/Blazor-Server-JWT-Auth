using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml;

namespace BlazorServerJWTAuth.Authentication.Helpers
{
    public static class RsaCryptoExtensions
    {
        public static void FromXmlRsaString_PublicOnly(this RSACryptoServiceProvider rsa, string xmlPublicKeyString)
        {
            RSAParameters parameters = new RSAParameters();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlPublicKeyString);

            #region RSA Algorithm, XML Structure and Properties

            /* RSA Algorithm and  XML Structure
            -------------------------------
            RSA Algorithm
            To generate a key pair, you start by creating two large prime numbers named p and q.
            These numbers are multiplied and the result is called n.
            Because p and q are both prime numbers, the only factors of n are 1, p, q, and n.
            -------------------------------
            <RSAKeyValue>
               <Modulus>…</Modulus>
               <Exponent>…</Exponent>
               <P>…</P> 
               <Q>…</Q>
               <DP>…</DP>
               <DQ>…</DQ>
               <InverseQ>…</InverseQ>
               <D>…</D>
            </RSAKeyValue>
            SUMMARY OF FIELDS
            D	        ==          d, the private exponent	privateExponent
            DP	        ==          d mod (p - 1)	exponent1
            DQ	        ==          d mod (q - 1)	exponent2
            Exponent    ==	        e, the public exponent	publicExponent
            InverseQ    ==	        (InverseQ)(q) = 1 mod p	coefficient
            Modulus     ==	        n	modulus
            P           ==  	    p	prime1
            Q	        ==          q	prime2
            The security of RSA derives from the fact that, given the public key { e, n }, it is computationally infeasible to calculate d, either directly or by factoring n into p and q. Therefore, any part of the key related to d, p, or q must be kept secret. If you call
            ExportParameters and ask for only the public key information, this is why you will receive only Exponent and Modulus. The other fields are available only if you have access to the private key, and you request it.
            */

            #endregion

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus": parameters.Modulus = Convert.FromBase64String(node.InnerText); break;
                        case "Exponent": parameters.Exponent = Convert.FromBase64String(node.InnerText); break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
        }

    }
}
