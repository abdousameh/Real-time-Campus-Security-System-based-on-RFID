using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Impinj.OctaneSdk;
using System.Windows.Media;

namespace Octane_ViewModel
{
    public class TagWpf : Tag
    {
        public int ManagedThreadId { get; set; }
        public string EpcFormatted { get; set; }
        public Brush Color { get; set; }
        public DateTime Timestamp { get; set; }

        internal static TagWpf Convert(Tag tag)
        {
            var tagWpf = new TagWpf();

            tagWpf.AntennaPortNumber = tag.AntennaPortNumber;
            tagWpf.ChannelInMhz = tag.ChannelInMhz;
            tagWpf.Epc = tag.Epc;
            tagWpf.EpcFormatted = _formatEpc(tag.Epc);
            tagWpf.FirstSeenTime = tag.FirstSeenTime;
            tagWpf.IsAntennaPortNumberPresent = tag.IsAntennaPortNumberPresent;
            tagWpf.IsChannelInMhzPresent = tag.IsChannelInMhzPresent;
            tagWpf.IsFirstSeenTimePresent = tag.IsFirstSeenTimePresent;
            tagWpf.IsLastSeenTimePresent = tag.IsLastSeenTimePresent;
            tagWpf.IsPeakRssiInDbmPresent = tag.IsPeakRssiInDbmPresent;
            tagWpf.IsPhaseAngleInRadiansPresent = tag.IsPhaseAngleInRadiansPresent;
            tagWpf.IsSeenCountPresent = tag.IsSeenCountPresent;
            tagWpf.IsSerializedTidPresent = tag.IsSerializedTidPresent;
            tagWpf.LastSeenTime = tag.LastSeenTime;
            tagWpf.PeakRssiInDbm = tag.PeakRssiInDbm;
            tagWpf.PhaseAngleInRadians = tag.PhaseAngleInRadians;
            tagWpf.Rank = tag.Rank;
            tagWpf.ReaderIdentity = tag.ReaderIdentity;
            tagWpf.SeenCount = tag.SeenCount;
            tagWpf.SerializedTid = tag.SerializedTid;

            return tagWpf;
        }

        private static string _formatEpc(string epc)
        {
            var result = string.Empty;

            if(epc.Length <= 4)
            {
                result = epc;
            }
            else if (epc.Length > 4 && epc.Length <= 8)
            {
                result = string.Format("{0}-{1}", epc.Substring(0, 4), epc.Substring(4));
            }
            else if (epc.Length > 8 && epc.Length <= 12)
            {
                result = string.Format("{0}-{1}-{2}", epc.Substring(0, 4), epc.Substring(4, 4), epc.Substring(8));
            }
            else if (epc.Length > 12 && epc.Length <= 16)
            {
                result = string.Format("{0}-{1}-{2}-{3}", epc.Substring(0, 4), epc.Substring(4, 4), epc.Substring(8, 4), epc.Substring(12));
            }
            else if (epc.Length > 16 && epc.Length <= 20)
            {
                result = string.Format("{0}-{1}-{2}-{3}-{4}", epc.Substring(0, 4), epc.Substring(4, 4), epc.Substring(8, 4), epc.Substring(12, 4), epc.Substring(16));
            }
            else if (epc.Length > 20 && epc.Length <= 24)
            {
                result = string.Format("{0}-{1}-{2}-{3}-{4}-{5}", epc.Substring(0, 4), epc.Substring(4, 4), epc.Substring(8, 4), epc.Substring(12, 4), epc.Substring(16, 4), epc.Substring(20));
            }

            return result;
        }
    }
}
