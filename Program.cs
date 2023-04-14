using System;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using PancakeSwapNET;
using System.Security.Principal;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.RPC.Eth;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Nethereum.BlockchainProcessing.BlockStorage.Entities;
using Org.BouncyCastle.Asn1.X509;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IO;

class Program
{
    static async Task Main()
    {
        // Địa chỉ của tài khoản mua token
        string buyerAddress = "0x532B421C1EF2cC03643F2181686A6363f511c3FB";

        // Địa chỉ của tài khoản nhận token
        string recipientAddress = "0x532B421C1EF2cC03643F2181686A6363f511c3FB";

        // Khởi tạo đối tượng Web3 để tương tác với blockchain
        var web3 = new Web3(new Account(buyerAddress), "https://data-seed-prebsc-1-s1.binance.org:8545/");

        // Số lượng token muốn mua
        decimal amountToBuy = 0.001m;

        // Địa chỉ hợp đồng của token trên PancakeSwap
        //string tokenContractAddress = "0x3ee2200efb3400fabb9aacf31297cbdd1d435d47";
        Console.WriteLine($"Nhập địa chỉ hợp đồng:  ");
        string tokenContractAddress = Console.ReadLine();

        // Địa chỉ hợp đồng của PancakeSwap Router
        var routerContractAddress = "0x10ED43C718714eb63d5aA57B78B54704E256024E";
        
        // Lấy giá trị số dư tài khoản
        var balance = await web3.Eth.GetBalance.SendRequestAsync(buyerAddress);
        decimal etherBalance = Web3.Convert.FromWei(balance.Value);
        Console.WriteLine($"Số dư: {balance} ");

        // Đơn vị tiền tệ 
        string spend = "0xbb4CdB9CBd36B01bD1cBaEBF2De08d9173bc095c";

        // Kiểm tra số dư Ether của tài khoản mua token
        if (etherBalance < amountToBuy)
        {
            Console.WriteLine("Không đủ Ether để mua token.");
            return;
        }
        else
        {
            Console.WriteLine($"Ether để mua token: {etherBalance}");
        }

        // Địa chỉ người bán token trên PancakeSwap
        string sellerAddress = "0x261AF0030618a52FA767997ed310174b3Bc3B77F";

        // Tạo đối tượng Swap trên PancakeSwap
        var swap = new Swap(web3, routerContractAddress);

        // Thời hạn hoàn thành giao dịch
        var deadline = DateTime.Now.AddMinutes(20); 
        
        // Mua token từ người bán
        var txHash = Swap.SwapExactETHForTokens(0, spend, amountToBuy, tokenContractAddress, recipientAddress, deadline);

        // In ra mã giao dịch
        Console.WriteLine($"Giao dịch thành công. Mã giao dịch: {txHash}");

    }
}
