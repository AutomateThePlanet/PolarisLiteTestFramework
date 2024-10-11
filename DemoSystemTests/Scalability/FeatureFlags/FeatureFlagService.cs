using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSystemTests.Scalability.FeatureFlags;
public static class FeatureFlagService
{
    private static readonly Dictionary<FeatureFlags, bool> _defaultFeatureFlags;
    private static readonly Dictionary<FeatureFlags, bool> _featureFlags;

    // Thread-local event handlers for each event
    private static readonly ThreadLocal<EventHandler<FeatureFlagEventArgs>> _flagToggled = new ThreadLocal<EventHandler<FeatureFlagEventArgs>>();
    private static readonly ThreadLocal<EventHandler<FeatureFlagEventArgs>> _flagRestored = new ThreadLocal<EventHandler<FeatureFlagEventArgs>>();

    static FeatureFlagService()
    {
        // Initialize default feature flags
        _defaultFeatureFlags = new Dictionary<FeatureFlags, bool>
            {
                { FeatureFlags.EnableDarkMode, false },
                { FeatureFlags.EnableBetaFeatures, false },
                { FeatureFlags.EnableNewCheckoutFlow, false },
                { FeatureFlags.EnablePromotions, true },
                { FeatureFlags.EnableCountrySpecificFeatures, false },
                { FeatureFlags.Enable2FA, true },
                { FeatureFlags.EnableAnalyticsTracking, true },
                { FeatureFlags.EnableABTesting, false }
            };

        // Initialize runtime feature flags (can be changed during test execution)
        _featureFlags = new Dictionary<FeatureFlags, bool>(_defaultFeatureFlags);
    }

    // Thread-local events
    public static event EventHandler<FeatureFlagEventArgs> FlagToggled
    {
        add
        {
            if (!_flagToggled.IsValueCreated)
            {
                _flagToggled.Value = value;
            }
            else
            {
                _flagToggled.Value += value;
            }
        }
        remove
        {
            if (_flagToggled.IsValueCreated)
            {
                _flagToggled.Value -= value;
            }
        }
    }

    public static event EventHandler<FeatureFlagEventArgs> FlagRestored
    {
        add
        {
            if (!_flagRestored.IsValueCreated)
            {
                _flagRestored.Value = value;
            }
            else
            {
                _flagRestored.Value += value;
            }
        }
        remove
        {
            if (_flagRestored.IsValueCreated)
            {
                _flagRestored.Value -= value;
            }
        }
    }

    // Static methods for accessing feature flags
    public static bool GetFeatureFlag(FeatureFlags flag)
    {
        var value = _featureFlags[flag];
        return value;
    }

    public static void SetFeatureFlag(FeatureFlags flag, bool isEnabled)
    {
        _featureFlags[flag] = isEnabled;
        OnFlagToggled(flag, isEnabled);  // Raise the event
    }

    public static void RestoreDefaultFeatureFlag(FeatureFlags flag)
    {
        _featureFlags[flag] = _defaultFeatureFlags[flag];
        OnFlagRestored(flag, _defaultFeatureFlags[flag]);  // Raise the event
    }

    public static Dictionary<FeatureFlags, bool> GetAllFeatureFlags()
    {
        return _featureFlags;
    }

    // Static methods to raise events
    private static void OnFlagToggled(FeatureFlags flag, bool isEnabled)
    {
        _flagToggled.Value?.Invoke(null, new FeatureFlagEventArgs(flag, isEnabled));
    }

    private static void OnFlagRestored(FeatureFlags flag, bool isEnabled)
    {
        _flagRestored.Value?.Invoke(null, new FeatureFlagEventArgs(flag, isEnabled));
    }
}