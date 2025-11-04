using System;
using System.Collections.Generic;
using System.Linq;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Areas.Identity.Data;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Models;

namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Services
{
    public class RecommendationService
    {
        private readonly GraminDBContext _context;

        public RecommendationService(GraminDBContext context)
        {
            _context = context;
        }

        public List<Product> GetSimilarProducts(int productId, int topN = 5)
        {
            var allProducts = _context.Products.ToList();
            var currentProduct = allProducts.FirstOrDefault(p => p.ProductId == productId);
            if (currentProduct == null) return new List<Product>();

            // Combine text fields for simple bag-of-words
            var texts = allProducts.Select(p => $"{p.ProductName} {p.Description}").ToList();

            // Build vocabulary
            var vocab = texts
                .SelectMany(t => t.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .Select(w => w.ToLower())
                .Distinct()
                .ToList();

            // Convert each product text into a vector
            List<double[]> vectors = texts.Select(text =>
            {
                var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(w => w.ToLower());
                double[] vector = new double[vocab.Count];
                for (int i = 0; i < vocab.Count; i++)
                    vector[i] = words.Count(w => w == vocab[i]);
                return vector;
            }).ToList();

            int currentIndex = allProducts.FindIndex(p => p.ProductId == productId);
            double[] currentVector = vectors[currentIndex];

            // Compute cosine similarity
            double[] similarities = vectors.Select(v => CosineSimilarity(currentVector, v)).ToArray();

            // Return top N similar items
            var recommended = allProducts
                .Zip(similarities, (product, score) => new { product, score })
                .Where(x => x.product.ProductId != productId)
                .OrderByDescending(x => x.score)
                .Take(topN)
                .Select(x => x.product)
                .ToList();

            return recommended;
        }

        private double CosineSimilarity(double[] v1, double[] v2)
        {
            double dot = 0;
            double normA = 0;
            double normB = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                dot += v1[i] * v2[i];
                normA += v1[i] * v1[i];
                normB += v2[i] * v2[i];
            }
            return normA == 0 || normB == 0 ? 0 : dot / (Math.Sqrt(normA) * Math.Sqrt(normB));
        }
    }
}
