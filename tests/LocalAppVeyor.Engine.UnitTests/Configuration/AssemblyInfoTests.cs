﻿using FluentAssertions;
using LocalAppVeyor.Engine.Configuration;
using LocalAppVeyor.Engine.Configuration.Reader;
using Xunit;
using Xunit.Abstractions;

namespace LocalAppVeyor.Engine.UnitTests.Configuration
{
    public class AssemblyInfoTests : BaseTestClass
    {
        public AssemblyInfoTests(ITestOutputHelper outputter) 
            : base(outputter)
        {
        }

        [Fact]
        public void ShouldReadAssemblyInfoStep()
        {
            const string yaml = @"
assembly_info:
  patch: true
  file: AssemblyInfo.*
  assembly_version: ""2.2.{ build}""
  assembly_file_version: ""{version}""
  assembly_informational_version: ""{version}""
";

            var conf = new BuildConfigurationYamlStringReader(yaml).GetBuildConfiguration();

            conf.AssemblyInfo.ShouldBeEquivalentTo(new AssemblyInfo(
                true,
                "AssemblyInfo.*",
                "2.2.{ build}",
                "{version}",
                "{version}"));
        }

        [Fact]
        public void ShouldBePatchFalseForAssemblyInfoWhenNotSpecified()
        {
            var conf = new BuildConfigurationYamlStringReader(string.Empty).GetBuildConfiguration();

            conf.AssemblyInfo.ShouldBeEquivalentTo(new AssemblyInfo());
            conf.AssemblyInfo.ShouldBeEquivalentTo(new AssemblyInfo(
                false,
                null,
                null,
                null,
                null));
        }
    }
}