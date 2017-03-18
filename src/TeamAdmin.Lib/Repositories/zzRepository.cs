using AutoMapper;
using System;
using TeamAdmin.Lib.Repositories.EFContext;
using TeamAdmin.Lib.zz;

namespace TeamAdmin.Lib.Repositories
{
    public class zzRepository
    {
        IMapper mapper;
        public zzRepository()
        {
            mapper = AutoMapperFactory.GetMapper();
        }

        public void Save(TryOutModel model)
        {
            var formdata = mapper.Map<EFContext.zzFormData>(model);
            using (var context = ContextFactory.Create<zzContext>())
            {
                formdata.DateCreated = DateTime.UtcNow;
                context.FormData.Add(formdata);
                context.SaveChanges();
            }
        }

        public void Save(SummerCampRegistration model)
        {
            var formdata = mapper.Map<EFContext.zzFormData>(model);
            using (var context = ContextFactory.Create<zzContext>())
            {
                formdata.DateCreated = DateTime.UtcNow;
                context.FormData.Add(formdata);
                context.SaveChanges();
            }
        }
    }
}
