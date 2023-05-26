using AutoMapper;
using Interfaces.Map;
using Interfaces.Service;
using Models;
using System;
using System.Collections.Generic;
using ViewModels.ViewModels;

namespace Maps
{
    public class PharmacyMap : IPharmacyMap
    {

        IPharmacyService pharmacyService;
        IMapper iMapper;

        public PharmacyMap(IPharmacyService service)
        {
            pharmacyService = service;
            var config = new MapperConfiguration(cfg =>
            {
                
                cfg.CreateMap<PharmacyMedicine, PharmacyMedicineViewModel>();
                cfg.CreateMap<Medicine, MedicineViewModel>();
                cfg.CreateMap<Pharmacy, PharmacyViewModel>();

                cfg.CreateMap<PharmacyMedicineViewModel, PharmacyMedicine>();
                cfg.CreateMap<MedicineViewModel, Medicine>();
                cfg.CreateMap<PharmacyViewModel, Pharmacy>();

            });
            config.AssertConfigurationIsValid();
            iMapper = config.CreateMapper();


        }

        public void AddMedicine(MedicineViewModel medicine, int currentUserId)
        {
           pharmacyService.AddMedicine(iMapper.Map<MedicineViewModel, Medicine>(medicine), currentUserId);
        }



        public void AddPharmacy(PharmacyViewModel pharmacy, int currentUserId)
        {
            pharmacyService.AddPharmacy(iMapper.Map<PharmacyViewModel, Pharmacy>(pharmacy), currentUserId);

        }

        public void DeleteMedicine(MedicineViewModel medicine, int currentUserId)
        {
            pharmacyService.DeleteMedicine(iMapper.Map<MedicineViewModel, Medicine>(medicine), currentUserId);
        }


        public void DeletePharmacy(PharmacyViewModel pharmacy, int currentUserId)
        {
            pharmacyService.DeletePharmacy(iMapper.Map<PharmacyViewModel, Pharmacy>(pharmacy), currentUserId);

        }

        public List<MedicineViewModel> GetAllMedicine(int currentUserID)
        {
            return iMapper.Map<List<Medicine>, List<MedicineViewModel>>(pharmacyService.GetAllMedicine(currentUserID));

        }

        public List<PharmacyMedicineViewModel> GetAllMedicineForPharmacy(int pharmacyId, int currentUserId)
        {
            return iMapper.Map<List<PharmacyMedicine>, List<PharmacyMedicineViewModel>>(pharmacyService.GetAllMedicineForPharmacy(pharmacyId, currentUserId));

        }

        public List<PharmacyViewModel> GetAllPharmacies(int currentUserID)
        {
            return iMapper.Map<List<Pharmacy>, List<PharmacyViewModel>>(pharmacyService.GetAllPharmacies(currentUserID));

        }

        public void UpdateMedicine(MedicineViewModel medicine, int currentUserId)
        {
            pharmacyService.UpdateMedicine(iMapper.Map<MedicineViewModel, Medicine>(medicine), currentUserId);
        }

        public void UpdateMedicineInPharmacy(PharmacyMedicineViewModel medicine, int currentUserId)
        {
            pharmacyService.UpdateMedicineInPharmacy(iMapper.Map<PharmacyMedicineViewModel, PharmacyMedicine>(medicine), currentUserId);

        }

        public void UpdatePharmacy(PharmacyViewModel pharmacy, int currentUserId)
        {
            pharmacyService.UpdatePharmacy(iMapper.Map<PharmacyViewModel, Pharmacy>(pharmacy), currentUserId);

        }
    }
}
