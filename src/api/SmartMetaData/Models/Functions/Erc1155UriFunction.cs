using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using SmartMetaData.Attributes;

namespace SmartMetaData.Models.Functions;

[Function("uri", "string"), FunctionHash("0x0e89341c")]
public class Erc1155UriFunction : Function
{
    [Parameter("uint256", "id", 1)]
    public BigInteger Id { get; set; }
}
