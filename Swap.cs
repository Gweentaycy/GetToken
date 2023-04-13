using Nethereum.Web3;
using System.Numerics;

internal class Swap
{
    internal object GetFunction;
    private Web3 web3;
    private string routerContractAddress;

    public Swap(Web3 web3, string routerContractAddress)
    {
        this.web3 = web3;
        this.routerContractAddress = routerContractAddress;
    }
    // Phương thức mua token từ người bán
    public static string SwapExactETHForTokens(decimal Amount,string spend,  decimal amountToBuy, string tokenContractAddress, string recipientAddress, DateTime deadline)
    {
        throw new NotImplementedException();
    }

}