# Advent of Code

This are all my solutions I've done for Advent of Code.

* 2022 => 13 ⭐⭐ (I'm trying to use linq as much as possible, so some stuff might look strange)
* 2021 => 2 ⭐⭐ (I used this to test `AoC.Framework`)
* 2018 => 14 ⭐⭐ (Didn't have more time to continue AoC every day)
* 2017 => 25 ⭐⭐ (Whoo!)

# AoC.Framework

This is a little helper library for my [Advent of Code](https://adventofcode.com) adventure.

The whole reason I created this library is because my BF showed off his great Unit Tests for his AoC solution and I really liked that!

# Disclaimer

This library does follow the automation guidelines on the /r/adventofcode [community wiki](https://www.reddit.com/r/adventofcode/wiki/faqs/automation). 

Specifically:

* Outbound calls are cached and will only happen when something changes (For /20XX/day/XX). See `AoC.Framework/AoCHttpClient.cs` and `AoC.Framework/AoCCache.cs`
  * More specifically, the script downloads the page:
    * once at the beginning
    * once I submit part 1 correctly
    * once I submit part 2 correctly
  * Then it knows all the answers and won't bother the website anymore
* Failed tries are also cached and won't bother the api anymore unless the "retry" or "wrong page?" error happens. See `AoC.Framework/AoC.cs`
* Once inputs are downloaded, they are cached locally. See `AoC.Framework/AoCHttpClient.cs` and `AoC.Framework/AoCCache.cs`
* The User-Agent header in `AoC.Framework/Extensions/ServiceCollectionExtensions.cs` is set to me
