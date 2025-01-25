using Core.Event.Models;
using Core.Generator;
using Core.Repeat.Models;
using FluentAssertions;
using Infrastructure.Enums;
using NUnit.Framework;

namespace Tests.GeneratorEventTests;

public class GeneratorEventTests
{
    [TestCaseSource(nameof(TestCaseSources))]
    public void GeneratorEventTest(TestCase testCase, Expected expected)
    {
        var eventDto = InitializeEventDto(testCase);

        var generate = new EventGenerator();

        var generatedEvents = generate.Generate(eventDto);
        var generateStartDates = generatedEvents.Select(e => e.StartDateTime).ToList();
        var generateEndDates = generatedEvents.Select(e => e.EndDateTime).ToList();
        generateStartDates.Should().BeEquivalentTo(expected.EventStartTimes);
        generateEndDates.Should().BeEquivalentTo(expected.EventEndTimes);
    }

    #region Initialization

    private static EventDto InitializeEventDto(TestCase testCase)
    {
        return new EventDto
        {
            Title = "Test",
            StartDateTime = testCase.EventStartTime,
            EndDateTime = testCase.EventEndTime,
            Repeat = new RepeatDto
            {
                DateStart = testCase.EventStartTime,
                DateEnd = testCase.RepeatEndTime,
                Interval = testCase.RepeatInterval,
                IntervalType = testCase.RepeatIntervalType
            }
        };
    } 

    #endregion

    #region TestCases
    
    public record TestCase
    {
        public required DateTime EventStartTime { get; init; }
        
        public required DateTime EventEndTime { get; init; }
        
        public required int RepeatInterval { get; init; }
        
        public required IntervalTypes RepeatIntervalType { get; init; }
        
        public required DateTime RepeatEndTime { get; init; }
    }

    public record Expected
    {
        public required List<DateTime> EventStartTimes { get; init; }
        
        public required List<DateTime> EventEndTimes { get; init; }
    }

    private static IEnumerable<TestCaseData> TestCaseSources => new[]
    {
        new TestCaseData(
            new TestCase
            {
                EventStartTime = DateTime.Parse("2025-01-25 10:00:00"),
                EventEndTime = DateTime.Parse("2025-01-25 11:00:00"),
                RepeatInterval = 2,
                RepeatIntervalType = IntervalTypes.Day,
                RepeatEndTime = DateTime.Parse("2025-02-02 11:00:00")
            },
            new Expected
            {
                EventStartTimes =
                [
                    DateTime.Parse("2025-01-27 10:00:00"),
                    DateTime.Parse("2025-01-29 10:00:00"),
                    DateTime.Parse("2025-01-31 10:00:00"),
                    DateTime.Parse("2025-02-02 10:00:00"),
                ],
                EventEndTimes = 
                [
                    DateTime.Parse("2025-01-27 11:00:00"),
                    DateTime.Parse("2025-01-29 11:00:00"),
                    DateTime.Parse("2025-01-31 11:00:00"),
                    DateTime.Parse("2025-02-02 11:00:00"),
                ]
            }
        )
        .SetName("01. Мероприятие должно повторятся каждые два дня"),
        
        new TestCaseData(
            new TestCase
            {
                EventStartTime = DateTime.Parse("2025-01-25 10:00:00"),
                EventEndTime = DateTime.Parse("2025-01-25 11:00:00"),
                RepeatInterval = 2,
                RepeatIntervalType = IntervalTypes.Week,
                RepeatEndTime = DateTime.Parse("2025-02-22 11:00:00")
            },
            new Expected
            {
                EventStartTimes =
                [
                    DateTime.Parse("2025-02-08 10:00:00"),
                    DateTime.Parse("2025-02-22 10:00:00"),
                ],
                EventEndTimes = 
                [
                    DateTime.Parse("2025-02-08 11:00:00"),
                    DateTime.Parse("2025-02-22 11:00:00"),
                ]
            }
        )
        .SetName("02. Мероприятие должно повторятся каждые две недели"),
        
        new TestCaseData(
            new TestCase
            {
                EventStartTime = DateTime.Parse("2025-01-25 10:00:00"),
                EventEndTime = DateTime.Parse("2025-01-25 11:00:00"),
                RepeatInterval = 1,
                RepeatIntervalType = IntervalTypes.Month,
                RepeatEndTime = DateTime.Parse("2025-05-22 11:00:00")
            },
            new Expected
            {
                EventStartTimes =
                [
                    DateTime.Parse("2025-02-25 10:00:00"),
                    DateTime.Parse("2025-03-25 10:00:00"),
                    DateTime.Parse("2025-04-25 10:00:00"),
                ],
                EventEndTimes = 
                [
                    DateTime.Parse("2025-02-25 11:00:00"),
                    DateTime.Parse("2025-03-25 11:00:00"),
                    DateTime.Parse("2025-04-25 11:00:00"),
                ],
            }
        )
        .SetName("03. Мероприятие должно повторятся каждый месяц"),
        
        new TestCaseData(
            new TestCase
            {
                EventStartTime = DateTime.Parse("2025-01-25 10:00:00"),
                EventEndTime = DateTime.Parse("2025-01-25 11:00:00"),
                RepeatInterval = 1,
                RepeatIntervalType = IntervalTypes.Year,
                RepeatEndTime = DateTime.Parse("2026-02-22 11:00:00")
            },
            new Expected
            {
                EventStartTimes =
                [
                    DateTime.Parse("2026-01-25 10:00:00"),
                ],
                EventEndTimes =
                [
                    DateTime.Parse("2026-01-25 11:00:00"),
                ]
            }
        )
        .SetName("04. Мероприятие должно повторятся каждый год"),
    };
    
    #endregion
}