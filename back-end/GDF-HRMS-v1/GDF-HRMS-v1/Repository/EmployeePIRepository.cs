﻿using GDF_HRMS_v1.Data;
using GDF_HRMS_v1.Models;
using GDF_HRMS_v1.Models.Dtos;
using GDF_HRMS_v1.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GDF_HRMS_v1.Repository
{
    public class EmployeePIRepository : IEmployeePIRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeePIRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateEmployeePI(EmployeePI employeePI)
        {
            _db.EmployeePIs.Add(employeePI);
            return Save();
        }

        public bool CreateReligion(Religion religion)
        {
            _db.Religions.Add(religion);
            return Save();
        }

        public bool CreateCountry(Country country)
        {
            _db.Countries.Add(country);
            return Save();
        }

        public bool CreateNationality(Nationality nationality)
        {
            _db.Nationalities.Add(nationality);
            return Save();
        }

        public bool CreateDepartment(Department department)
        {
            _db.Departments.Add(department);
            return Save();
        }

        public bool CreateEthnicity(Ethnicity ethnicity)
        {
            _db.Ethnicities.Add(ethnicity);
            return Save();
        }

        public bool CreateMaritalStatus(MaritalStatus maritalStatus)
        {
            _db.MaritalStatuses.Add(maritalStatus);
            return Save();
        }

        public bool CreatePosition(Position position)
        {
            _db.Positions.Add(position);
            return Save();
        }

        public bool CreateRegion(Region region)
        {
            _db.Regions.Add(region);
            return Save();
        }

        public bool DeleteEmployeePI(EmployeePI employeePI)
        {
            _db.EmployeePIs.Remove(employeePI);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _db.Countries.Remove(country);
            return Save();
        }

        public bool EmployeePIExists(string name)
        {
            bool value = _db.EmployeePIs.Any(a => a.Lname.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ReligionExists(string name)
        {
            bool value = _db.Religions.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool CountryExists(string name)
        {
            bool value = _db.Countries.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool CountryExists(int Id)
        {
            bool value = _db.Countries.Any(a => a.Id == Id);
            return value;
        }
        public bool NationalityExists(string name)
        {
            bool value = _db.Nationalities.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool DepartmentExists(string name)
        {
            bool value = _db.Departments.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool EthnicityExists(string name)
        {
            bool value = _db.Ethnicities.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool MaritalStatusExists(string name)
        {
            bool value = _db.MaritalStatuses.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool PositionExists(string name)
        {
            bool value = _db.Positions.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool RegionExists(string name)
        {
            bool value = _db.Regions.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }
        public bool EmployeePIExists(int id)
        {
            bool value = _db.EmployeePIs.Any(a => a.Id == id);
            return value;
        }

        public ICollection<EmployeePIDto> GetEmployeePIByOtherCriteria(string employeeFname, string employeeLname, string employeePosition)
        {
            var employeeData = _db.CareerHistories.Where(a => a.EmployeePI.Fname == employeeFname || a.EmployeePI.Lname == employeeLname || a.Position.Name == employeePosition)
                .Join(
                    _db.EmployeePIs,
                    careerHistory => careerHistory.EId,
                    employeePI => employeePI.Id,
                    (careerHistory, employeePI) => new
                    {
                        CareerHistoryId = careerHistory.Id,
                        EmployeePIFname = employeePI.Fname,
                        EmployeePILname = employeePI.Lname,
                        EmployeePIOname = employeePI.Oname,
                        EmployeePINidNumber = employeePI.NidNumber,
                        EmployeePIPNumber = employeePI.PNumber,
                        EmployeePIPExpirationDate = employeePI.PExpirationDate,
                        EmployeePISex = employeePI.Sex,
                        EmployeePITinNumber = employeePI.TinNumber,
                        EmployeePITitle = employeePI.Title,
                        EmployeePIDob = employeePI.Dob,
                        EmployeePIReligion = employeePI.Religion,
                        EmployeePINationality = employeePI.Nationality,
                        EmployeePIEthnicity = employeePI.Ethnicity,
                        EmployeePIMaritalStatus = employeePI.MaritalStatus,
                        EmployeePICountry = employeePI.Address.Country.Name,
                        EmployeePIRegion = employeePI.Address.Region.Name,
                        EmployeePIRNumber = employeePI.RNumber,
                        EmployeePILot = employeePI.Address.Lot,
                        EmployeePIArea = employeePI.Address.Area,
                        EmployeePIStreet = employeePI.Address.Street,
                        EmployeePIVillage = employeePI.Address.Village,
                        EmployeePIHNumber = employeePI.ContactInfo.HNumber,
                        EmployeePICNumber = employeePI.ContactInfo.CNumber,
                        EmployeePIWNumber = employeePI.ContactInfo.WNumber,
                        EmployeePIEmail = employeePI.ContactInfo.Email,
                        CareerHistoryPosition = careerHistory.Position.Name,
                        CareerHistoryDepartmentName = careerHistory.Department.Name,
                        CareerHistoryDepartmentDescription = careerHistory.Department.Description,
                        CareerHistoryDepartmentLocation = careerHistory.Department.Location
                    }
                ).ToList();
            return employeeData.Select(a => new EmployeePIDto
            {
                FirstName = a.EmployeePIFname,
                LastName = a.EmployeePILname,
                OtherName = a.EmployeePIOname,
                NationalIdNumber = a.EmployeePINidNumber,
                PassportNumber = a.EmployeePIPNumber,
                PassportExpirationDate = a.EmployeePIPExpirationDate,
                Sex = a.EmployeePISex,
                TinNumber = a.EmployeePITinNumber,
                Title = a.EmployeePITitle,
                DateOfBirth = a.EmployeePIDob,
                Religion = a.EmployeePIReligion.Name,
                Nationality = a.EmployeePINationality.Name,
                Ethnicity = a.EmployeePIEthnicity.Name,
                MaritalStatus = a.EmployeePIMaritalStatus.Name,
                Country = a.EmployeePICountry,
                Position = a.CareerHistoryPosition,
                Region = a.EmployeePIRegion,
                RegimentNumber = a.EmployeePIRNumber,
                AddressLot = a.EmployeePILot,
                AddressArea = a.EmployeePIArea,
                AddressStreet = a.EmployeePIStreet,
                AddressVillage = a.EmployeePIVillage,
                HomeNumber = a.EmployeePIHNumber,
                CellNumber = a.EmployeePICNumber,
                WorkNumber = a.EmployeePIWNumber,
                Email = a.EmployeePIEmail,
                DepartmentName = a.CareerHistoryDepartmentName,
                DepartmentDescription = a.CareerHistoryDepartmentDescription,
                DepartmentLocation = a.CareerHistoryDepartmentLocation
            }).ToList();
        }

        
        public EmployeePI GetEmployeePIById(int employeeId)
        {
            return _db.EmployeePIs.FirstOrDefault(a => a.Id == employeeId);
        }

        public Country GetCountryById(int countryId)
        {
            return _db.Countries.FirstOrDefault(a => a.Id == countryId);
        }
        public EmployeePIDto GetEmployeePIByRegNumber(int employeeRNumber)
        {
            var employeeData = _db.EmployeePIs.Where(a => a.RNumber.Equals(employeeRNumber))
                .Join(
                    _db.CareerHistories,
                    employeePI => employeePI.Id,
                    careerHistory => careerHistory.EmployeePI.Id,
                    (employeePI, careerHistory) => new
                    {
                        CareerHistoryId = careerHistory.Id,
                        EmployeePIFname = employeePI.Fname,
                        EmployeePILname = employeePI.Lname,
                        EmployeePIOname = employeePI.Oname,
                        EmployeePINidNumber = employeePI.NidNumber,
                        EmployeePIPNumber = employeePI.PNumber,
                        EmployeePIPExpirationDate = employeePI.PExpirationDate,
                        EmployeePISex = employeePI.Sex,
                        EmployeePITinNumber = employeePI.TinNumber,
                        EmployeePITitle = employeePI.Title,
                        EmployeePIDob = employeePI.Dob,
                        EmployeePIReligion = employeePI.Religion,
                        EmployeePINationality = employeePI.Nationality,
                        EmployeePIEthnicity = employeePI.Ethnicity,
                        EmployeePIMaritalStatus = employeePI.MaritalStatus,
                        EmployeePICountry = employeePI.Address.Country.Name,
                        EmployeePIRegion = employeePI.Address.Region.Name,
                        EmployeePIRNumber = employeePI.RNumber,
                        EmployeePILot = employeePI.Address.Lot,
                        EmployeePIArea = employeePI.Address.Area,
                        EmployeePIStreet = employeePI.Address.Street,
                        EmployeePIVillage = employeePI.Address.Village,
                        EmployeePIHNumber = employeePI.ContactInfo.HNumber,
                        EmployeePICNumber = employeePI.ContactInfo.CNumber,
                        EmployeePIWNumber = employeePI.ContactInfo.WNumber,
                        EmployeePIEmail = employeePI.ContactInfo.Email,
                        CareerHistoryPosition = careerHistory.Position.Name,
                        CareerHistoryDepartmentName = careerHistory.Department.Name,
                        CareerHistoryDepartmentDescription = careerHistory.Department.Description,
                        CareerHistoryDepartmentLocation = careerHistory.Department.Location
                    }
                );
            return employeeData.Select(a => new EmployeePIDto
            {
                FirstName = a.EmployeePIFname,
                LastName = a.EmployeePILname,
                OtherName = a.EmployeePIOname,
                NationalIdNumber = a.EmployeePINidNumber,
                PassportNumber = a.EmployeePIPNumber,
                PassportExpirationDate = a.EmployeePIPExpirationDate,
                Sex = a.EmployeePISex,
                TinNumber = a.EmployeePITinNumber,
                Title = a.EmployeePITitle,
                DateOfBirth = a.EmployeePIDob,
                Religion = a.EmployeePIReligion.Name,
                Nationality = a.EmployeePINationality.Name,
                Ethnicity = a.EmployeePIEthnicity.Name,
                MaritalStatus = a.EmployeePIMaritalStatus.Name,
                Country = a.EmployeePICountry,
                Position = a.CareerHistoryPosition,
                Region = a.EmployeePIRegion,
                RegimentNumber = a.EmployeePIRNumber,
                AddressLot = a.EmployeePILot,
                AddressArea = a.EmployeePIArea,
                AddressStreet = a.EmployeePIStreet,
                AddressVillage = a.EmployeePIVillage,
                HomeNumber = a.EmployeePIHNumber,
                CellNumber = a.EmployeePICNumber,
                WorkNumber = a.EmployeePIWNumber,
                Email = a.EmployeePIEmail,
                DepartmentName = a.CareerHistoryDepartmentName,
                DepartmentDescription = a.CareerHistoryDepartmentDescription,
                DepartmentLocation = a.CareerHistoryDepartmentLocation
            }).FirstOrDefault();

            //var result =  (from a in _db.EmployeePIs
            //              select new EmployeePIDto
            //              {

            //                  Id = a.Id,
            //                  Fname = a.Fname,
            //                  Lname = a.Lname,
            //                  RId = a.Religion.Id,
            //                  ReligionName = a.Religion.Name

            //              }).FirstOrDefault();
            //return result;



        }

        public ICollection<EmployeePI> GetEmployeePIs()
        {
            return _db.EmployeePIs.OrderBy(a => a.Lname).ToList();
        }

        public ICollection<Country> GetAllCountries()
        {
            return _db.Countries.OrderBy(a => a.Name).ToList();
        }

        public ICollection<Department> GetAllDepartments()
        {
            return _db.Departments.OrderBy(a => a.Name).ToList();
        }

        public ICollection<Region> GetAllRegions()
        {
            return _db.Regions.OrderBy(a => a.Id).ToList();
        }

        public ICollection<Religion> GetAllReligions()
        {
            return _db.Religions.OrderBy(a => a.Id).ToList();
        }

        public ICollection<Nationality> GetAllNationalities()
        {
            return _db.Nationalities.OrderBy(a => a.Id).ToList();
        }

        public ICollection<Ethnicity> GetAllEthnicities()
        {
            return _db.Ethnicities.OrderBy(a => a.Id).ToList();
        }

        public ICollection<Position> GetAllPositions()
        {
            return _db.Positions.OrderBy(a => a.Id).ToList();
        }

        public ICollection<MaritalStatus> GetAllMaritalStatus()
        {
            return _db.MaritalStatuses.OrderBy(a => a.Id).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateEmployeePI(EmployeePI employeePI)
        {
            _db.EmployeePIs.Update(employeePI);
            return Save();
        }
        public bool UpdateEmployeeCH(CareerHistory careerHistory)
        {
            _db.CareerHistories.Update(careerHistory);
            return Save();
        }
        public CareerHistory GetEmployeeCHByEId(int employeeId)
        {
            // return _db.CareerHistories.FirstOrDefault(a => a.EId == employeeId);
            return null;
        }
        public bool EmployeeCHExists(int id)
        {
            bool value = _db.CareerHistories.Any(a => a.Id == id);
            return value;
        }
    }
}