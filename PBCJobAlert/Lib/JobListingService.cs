using PBCJobAlert.Lib.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PBCJobAlert.Lib
{
    public class JobListingService
    {
        private const string URL = "https://secure.co.palm-beach.fl.us/OnlineJobs/Job/GetJobListing?type=N";
        public async Task<IEnumerable<JobListing>> GetJobListings()
        {
            HttpClient client = new HttpClient();
            var res = await client.GetFromJsonAsync<JobListingResult>(URL);
            var list = res.Data;
            foreach(var item in list)
            {
                int index = item.JobTitle.IndexOf(" <");
                if(index != -1)
                {
                    item.JobTitle = item.JobTitle.Substring(0, index);
                }
            }
            return list;
        }

        private class JobListingResult
        {
            public List<JobListing> Data { get; set; }
        }
    }
}
