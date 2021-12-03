using Nethereum.ABI.FunctionEncoding;
using Nethereum.Contracts;
using Nethereum.Hex.HexConvertors.Extensions;
using SmartMetaData.Infrastructure.Attributes;
using SmartMetaData.Infrastructure.Extensions;

namespace SmartMetaData.Infrastructure.Models.Functions;

public abstract class Function : FunctionMessage
{
    public string Encode()
    {
        var hash = GetHash().EnsureHexPrefix();
        var encoder = new FunctionCallEncoder();
        var encodedParameters = encoder.EncodeParametersFromTypeAttributes(GetType(), this);
        var encodedCall = encoder.EncodeRequest(hash, encodedParameters.ToHex());
        return encodedCall;
    }

    public string GetHash() => GetType().GetAttribute<FunctionHashAttribute>().Hash;
}
