using System;
using AutoMapper;

namespace Common
{
    public class DateTimeOffsetFormatter : IValueConverter<long, DateTimeOffset>
    {
        public DateTimeOffset Convert(long sourceMember, ResolutionContext context)
        {
            return DateTimeOffset.FromUnixTimeSeconds(sourceMember);
        }
    }
}