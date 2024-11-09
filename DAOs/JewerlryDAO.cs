using BOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class JewerlryDAO
    {
        private static JewerlryDAO instance = null;
        private readonly SilverJewelry2023DbContext _context;

        private JewerlryDAO()
        {
            _context = new SilverJewelry2023DbContext();
        }

        public static JewerlryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JewerlryDAO();
                }
                return instance;
            }
        }

        public async Task<List<SilverJewelry>> GetJewelries()
        {
            return await _context.SilverJewelries.Include(j => j.Category).ToListAsync();
        }

        public async Task<SilverJewelry> GetJewelry(string id)
        {
            return await _context.SilverJewelries.Include(j => j.Category).FirstOrDefaultAsync(j => j.SilverJewelryId == id);
        }

        //private string GeneratePlayerId()
        //{
        //    var random = new Random();
        //    var id = random.Next(10000, 99999);
        //    return "PL" + id.ToString();
        //}

        public async Task<List<Category>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<SilverJewelry> AddSilverAsync(SilverJewelry silverJewelry)
        {
            try
            {
                await _context.SilverJewelries.AddAsync(silverJewelry);
                await _context.SaveChangesAsync();
                return silverJewelry;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<SilverJewelry> UpdateSilverAsync(SilverJewelry silverJewelry)
        {

            try
            {
                var oldJewelry = await _context.SilverJewelries.FirstOrDefaultAsync(j => j.SilverJewelryId.Equals(silverJewelry.SilverJewelryId));
                if (oldJewelry == null)
                {
                    throw new Exception("jewelry not found");
                }

                oldJewelry.SilverJewelryName = silverJewelry.SilverJewelryName;
                oldJewelry.SilverJewelryDescription = silverJewelry.SilverJewelryDescription;
                oldJewelry.MetalWeight = silverJewelry.MetalWeight;
                oldJewelry.Price = silverJewelry.Price;
                oldJewelry.ProductionYear = silverJewelry.ProductionYear;
                oldJewelry.CreatedDate = silverJewelry.CreatedDate;
                oldJewelry.CategoryId = silverJewelry.CategoryId;

                _context.Update(oldJewelry);
                await _context.SaveChangesAsync();
                return oldJewelry;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<SilverJewelry> DeleteJewelry(string id)
        {
            try
            {
                var jewelry = await _context.SilverJewelries.FirstOrDefaultAsync(j => j.SilverJewelryId.Equals(id));
                if (jewelry == null)
                {
                    throw new Exception("jewelry not found");
                }

                _context.SilverJewelries.Remove(jewelry);
                await _context.SaveChangesAsync();
                return jewelry;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
