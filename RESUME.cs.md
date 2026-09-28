---
schema_version: 1
type: tests
file_count: 17
delete_recommendation_percent: 35
generated_date: 2026-09-29
---

## Description

Sbírka testovacích projektů (`Dates.Tests`, `UAParser.Tests`,
`Metaproject.PackageIndex.Functions.ParseCsprojFile.Tests`) pro knihovny,
jejichž zdrojové projekty dřív žily ve `sunamo.notmine`/`sunamo.notmine.wpf`
a postupně se stěhovaly do `PlatformIndependentNuGetPackages`/`WindowsNuGetPackages`.

2026-09-29 byl odstraněn sirotčí `XliffParser.Tests` - odpovídající zdrojový
`XliffParser` v tomhle repu neexistoval, jediný testovací case byl zakomentovaný
a netestoval vůbec API `XliffParser` (jen `XmlNamespacesHolder` ze `SunamoShared`
proti natvrdo zapsané cestě). Originální knihovna je `fmuecke/XliffParser`
(NuGet `fmdev.XliffParser`), už dřív forknutá a modernizovaná jako
`github.com/sunamo/SunamoXliffParser` v `PlatformIndependentNuGetPackages`,
včetně vlastních reálných testů `SunamoXliffParser.Tests`.
