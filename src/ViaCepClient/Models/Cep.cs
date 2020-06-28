namespace ViaCepClient.Models
{
    /// <summary>
    /// Cep represents a cep value
    /// </summary>
    public class Cep
    {
        /// <summary>
        /// Cep region (X0000-000)
        /// </summary>
        public short Region { get; private set; }

        /// <summary>
        /// Cep subregion (0X000-000)
        /// </summary>
        public short Subregion { get; private set; }

        /// <summary>
        /// Cep sector (00X00-000)
        /// </summary>
        public short Sector { get; private set; }

        /// <summary>
        /// Cep subsector (000X0-000)
        /// </summary>
        public short SubSector { get; private set; }

        /// <summary>
        /// Cep subsector divisor (0000X-000)
        /// </summary>
        public short SubsectorDivisor { get; private set; }

        /// <summary>
        /// Cep distribuition suffix (00000-XXX)
        /// </summary>
        public short DistribuitionSuffix { get; private set; }

        /// <summary>
        /// Cep represents a cep value
        /// </summary>
        public Cep(string cep)
        {
            
        }

        public override ToString()
        {
            return string.Format("{0:D1}{1:D1}{2:D1}{3:D1}{4:D1}-{5:D3}",
                                Region,
                                Subregion,
                                Sector,
                                SubSector,
                                SubsectorDivisor,
                                DistribuitionSuffix);
        }
    }
}