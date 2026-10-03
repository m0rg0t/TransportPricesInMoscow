# Portable source regressions

## Scope
WP7 and WP8 search now handle nullable title/price/description and null query. Current-culture case-insensitive matching is explicit. Tests cover each field, case, unmatched queries and cleared queries for both actual view models. Existing fares and Store/MvvmLight sample projects are unchanged.

## Run
From repository root, with .NET SDK 10.0.x:

```sh
dotnet run --project tests/Portable.Tests/Portable.Tests.csproj --configuration Release
```

No NuGet packages are restored: tests/NuGet.Config clears package sources. The SDK targets only this harness; original application project files, package references and framework targets are unchanged. C# 5 syntax is enforced. CI uses commit-pinned checkout/setup-dotnet, a read-only token, no persisted credentials and a ten-minute timeout. setup-dotnet installs only the official .NET 10 SDK for the isolated harness; it does not install legacy/native SDKs.

## What is (and is not) verified
The project links actual production source files directly; it does not copy their logic. MVVM notifications use a minimal event adapter. HTTP adapters throw on every call; JSON adapters throw on parsing/enumeration. Tests exercise only search over in-memory PriceItem fixtures, never LoadData, tokens, services or fare retrieval. Native app binaries are not linked. Windows-1251 production comments and original line endings are preserved; the harness explicitly selects CodePage 1251.

The unchanged original source at `27f4c617c4ae12fc7155fbc6afdf859a22d5fd37` failed the new synthetic regression harness before the fix; the final source passes. Additional reversal tests independently fail for each corrected notification/null path and for the asynchronous duplicate guard where applicable.

## Dependency upgrade boundary
Targets span Phone 7.1/8 and Windows Store with machine-specific Telerik and MVVM references plus differing BCL/HTTP/JSON dependencies. Upgrades require original-target asset resolution and a historical Windows build, particularly for Phone 7.1.

Microsoft documents [historical Windows/Phone toolchain requirements](https://learn.microsoft.com/en-us/visualstudio/releases/2022/port-migrate-and-upgrade-visual-studio-projects). [Newtonsoft.Json 13.0.4 package assets](https://www.nuget.org/packages/Newtonsoft.Json/13.0.4) must be evaluated against each original target; computed compatibility is not a native Phone assembly or an app validation. No full application build, native runtime/device test or exhaustive dependency vulnerability audit is claimed. The next decision is whether to recover the matching historical toolchain for compatible dependency work, or separately scope a supported-platform migration.
