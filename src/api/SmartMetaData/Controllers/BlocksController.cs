using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Nethereum.RPC.Eth.DTOs;
using SmartMetaData.Models.Enums;
using SmartMetaData.Services;
using SmartMetaData.Utils;

namespace SmartMetaData.Controllers;

[ApiController]
[Route("blocks")]
public class BlocksController : ControllerBase
{
    private readonly IBlockService _blockService;

    public BlocksController(IBlockService blockService)
    {
        _blockService = blockService;
    }

    [HttpGet("latest")]
    [ProducesResponseType(typeof(Block), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLatestBlock([FromQuery, Required] EthereumNetwork network)
    {
        var latestBlock = await _blockService.GetLatestBlock(network);
        return Ok(latestBlock);
    }

    [HttpGet("{blockNumber}")]
    [ProducesResponseType(typeof(Block), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBlockByNumber(
        [FromRoute, Required] string blockNumber,
        [FromQuery, Required] EthereumNetwork network)
    {
        var parsedBlockNumber = ParseUtils.ParseBigInteger(blockNumber);
        if (parsedBlockNumber.IsFailure)
            return BadRequest($"Invalid {nameof(blockNumber)}");

        var latestBlock = await _blockService.GetBlockByNumber(parsedBlockNumber.Value, network);
        return Ok(latestBlock);
    }
}
