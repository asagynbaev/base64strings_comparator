using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RadioFreeEurope.Data;
using RadioFreeEurope.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RadioFreeEurope.Services
{
    public class DiffService : IDiff
    {
        private readonly ILogger<DiffService> _logger;

        private readonly DiffDBContext _dbContext;

        public DiffService()
        {
        }

        public DiffService(DiffDBContext diffDBContext, ILogger<DiffService> logger)
        {
            (_dbContext, _logger) = (diffDBContext, logger);
        }

        public async Task<Response> AddDiffAsync(Diff diff)
        {
            try
            {
                Diff existing = await _dbContext.Diffs.Where(x => x.Id == diff.Id).Where(x => x.Type == diff.Type).FirstOrDefaultAsync();

                // I didn't find exact scenario for case if we've got another value for an existing ID
                // so, I decided to keep existing value for that ID and not rewrite it
                if(existing != null) 
                    return new Response(200, $"You can't rewrite values, please insert different ID");
                else
                {
                    await _dbContext.Diffs.AddAsync(diff);
                    await _dbContext.SaveChangesAsync();
                    _logger.LogInformation($"{diff.Id} with value {diff.Base64Value} and {diff.Type} type has been saved");
                    return new Response(200, $"diff with ID: {diff.Id} succesfully has been added");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Response(500, ex.Message);
            }
        }

        public async Task<Response> GetDiffAsync(int id)
        {
            try
            {
                Diff left = await _dbContext.Diffs.Where(x => x.Id == id).Where(x => x.Type == DiffType.Left).FirstOrDefaultAsync();
                Diff right = await _dbContext.Diffs.Where(x => x.Id == id).Where(x => x.Type == DiffType.Right).FirstOrDefaultAsync();

                if (left == null || right == null)
                    return new Response(200, $"Value with ID {id} has no left or right input");
                else if (left == null && right == null)
                    return new Response(200, $"No iputs with given ID: {id}");
                else return Compare(left, right);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Response(500, ex.Message);
            }
        }

        public Response Compare(Diff left, Diff right)
        {
            List<IsDiff> list = new();

            if (left.Base64Value.Length != right.Base64Value.Length) 
                return new Response(200, "inputs are of different size");
            else if (left.Base64Value == right.Base64Value) 
                return new Response(200, "inputs are equal");
            else
            {
                for (int i = 0; i < left.Base64Value.Length; i++)
                {
                    if (left.Base64Value[i] != right.Base64Value[i])
                        list.Add(new IsDiff(i, i + 1));
                }
                return new Response(200, "input with diffs", list);
            }
        }

    }
}
