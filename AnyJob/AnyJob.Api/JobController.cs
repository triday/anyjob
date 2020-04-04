using System;
using System.Collections.Generic;
using System.Text;
using YS.Knife.Api;

namespace AnyJob.Api
{
    public class JobController : ApiBase<IJobService>, IJobService
    {
        public bool Cancel(string executionId)
        {
           
        }

        public JobInfo Query(string executionId)
        {
            throw new NotImplementedException();
        }

        public JobInfo Start(StartInfo jobStartInfo)
        {
            throw new NotImplementedException();
        }
    }
}
