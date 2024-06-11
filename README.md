# Archived! I no longer own a G14, switched to Framework Laptop 16 instead.

# Zephyrus Control Center
Made mostly out of frustration with having to choose between functionality and eye-candy. I *will* have both, because I can. Also maybe to flex AvaloniaUI and reverse-engineering skills. Sometimes it's cool to remind yourself what you're really capable of. ^-^  

**TLDR, show me how it looks like**   
[*Fine.*](#showcase)

# Features
>## **Gorgeous UI**  
>Who said functional stuff can't look good? Thanks to AvaloniaUI, Zephyrus Control Center provides a rich, Windows 11-integrated user interface while letting the user interact with most of the proprietary ASUS functionality.

>## **Fan control**   
>Select one of the 3 built-in performance profiles. They configure both power draw limits and fan curves, matching whatever's embedded in the firmware, thus staying within the safe, warranty-covered limits.
>
>Feeling lucky? Perhaps need every last bit of the power your GA402 has? Go beyond what the manufacturer let you do with Armoury Crate and push the system to its absolute limits by toggling the fan overrides. They let you configure RPM for each fan separately to run **at a constant rate, regardless of the core temperatures**.

>## **Diagnostics**  
>Taking advantage of the built-in embedded controller's capabilities, Zephyrus Control Center provides accurate real-time temperature and fan speed measurements. For the adventurous people out there, there's a way to easily dump your ACPI tables and WMI register contents, if you feel like reverse-engineering more than what the author did so far. Or perhaps willing to contribute to the cause? Dump your ACPI tables and upload them to the Issues page, it'll help everyone involved better understand the hardware they're wielding.

>## **Graphics & display management**  
>Tweak your built-in display refresh rates *without* sacrificing the overdrive capability. Switch between discrete-exclusive and power-efficient modes using the provided intuitive user interface.

>## **Advanced power management**  
>Configure processor turbo-boost while plugged-in and on battery power. Set maximum AMD Package Power Tracking limits (called *Platform Power Targets* in ZCC). A research effort is currently ongoing to allow configuration of TDC and EDC limits, and discover what the currently unknown related WMI registers do as well.

>## **AURA & keybinds control**  
>Like your keyboard lighting? You can keep it with ZCC! This project allows you to not only configure the built-in keyboard color animations, but to synchronize the LED color with your existing system accent. A research effort is currently ongoing to allow total 'ASUS Optimization' service independence by talking directly with your hardware.
>
>In addition, you can configure M3, M4, Fn+F4 and Fn+F5 keys to become a media button of your choosing, or make it launch an application. This will very likely be expanded in the future with a plugin system, but no promises!

>## **AniMe Matrix control**  
>**NOTE:** At the time of writing, AniMe Matrix control module is not implemented.
>
>Got the high-end model with AniMe Matrix? Lucky you! Zephyrus Control Center allows you to easily load an image, a GIF, or create your own pluggable display module in order to display a clock, scroll-text or anything you can possibly imagine. The API is minimal and intuitive. Let's flip the middle finger towards ASUSTeK software division and create the ultimate experience that *we deserve, together*.

>## **Elegant, maintainable codebase**  
>Owing to the great deal of experience I've gathered over the last 10+ years as a software engineer, the codebase of this project is clean, simple to understand and easily extensible. This means you can clone the project, build it out-of-box and tweak it to your heart's content right away.

# Caveats
>## GA402-specific
>To keep the development scope as narrow and focused as possible, I've decided to only support the GA402 revision of the Zephyrus G14. I'm sorry if that lets you down, but I'm just a single person and I can't afford to support the hardware I don't own. I'm the primary target audience of this software. If that works for you, cool. If it doesn't, unfortunately, I don't care. Feel free to fork this project and develop it for your own purposes. It's why it's open-source.
>
>**Just remember to respect the license.** If you don't, we'll have a very unpleasant exchange.

>## Not-so-lightweight
>Compared to other projects, like [G-Helper](https://github.com/seerge/g-helper), this project provides superior user experience at the expense of more system resources. But that is a given with a pretty UI built on a framework in a pre-release status. In addition, there might be some memory leaks in dependency libraries that I don't have control over, so it's more of a waiting game to get them fixed.

>## Absolutely no guarantee it'll work for you
>Like mentioned earlier, I'm the target audience of this project. If it doesn't work for you, feel free to to post an issue, but I can almost guarantee I won't be fixing it unless it becomes an issue for myself.

# Showcase
![image](https://user-images.githubusercontent.com/4654533/227871101-d2a70df7-6923-46de-90c7-2859e6d6dc23.png)
![image](https://user-images.githubusercontent.com/4654533/227871244-e59944d3-fd29-4181-894c-2a74ca01e05a.png)
![image](https://user-images.githubusercontent.com/4654533/227871479-00142266-0fb0-464a-8dd9-7b050c68dcf2.png)
![image](https://user-images.githubusercontent.com/4654533/227871689-543ae30e-aa20-433a-86ad-5ab0f268b3b7.png)
![image](https://user-images.githubusercontent.com/4654533/227872150-f7b412d6-bb0e-4652-9abb-b585a1ed94af.png)
