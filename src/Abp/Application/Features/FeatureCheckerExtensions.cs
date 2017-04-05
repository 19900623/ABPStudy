using System;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Runtime.Session;
using Abp.Threading;

namespace Abp.Application.Features
{
    /// <summary>
    /// Some extension methods for <see cref="IFeatureChecker"/>.
    /// <see cref="IFeatureChecker"/>��չ����
    /// </summary>
    public static class FeatureCheckerExtensions
    {
        /// <summary>
        /// Gets value of a feature by it's name. This is sync version of <see cref="IFeatureChecker.GetValueAsync(string)"/>
        /// �������ƻ�ȡ����ֵ������<see cref="IFeatureChecker.GetValueAsync(string)"/>���첽�汾
        /// This is a shortcut for <see cref="GetValue(IFeatureChecker, int, string)"/> that uses <see cref="IAbpSession.TenantId"/> as tenantId.So, this method should be used only if TenantId can be obtained from the session.
        /// <see cref="GetValue(IFeatureChecker, int, string)"/>��<see cref="IAbpSession.TenantId"/>��Ϊ�⻧ID�Ŀ�ݷ�ʽ�����Դ˷���Ӧ�ñ�ʹ��Ϊ��Щ���Դ�session��ȡ�⻧IDֵ��
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="featureName">Unique feature name / ���ܵ�Ψһ����</param>
        /// <returns>Feature's current value / ���ܵĵ�ǰֵ</returns>
        public static string GetValue(this IFeatureChecker featureChecker, string featureName)
        {
            return AsyncHelper.RunSync(() => featureChecker.GetValueAsync(featureName));
        }

        /// <summary>
        /// Gets value of a feature by it's name. This is sync version of <see cref="IFeatureChecker.GetValueAsync(int, string)"/>
        /// �������ƻ�ȡ����ֵ������<see cref="IFeatureChecker.GetValueAsync(int, string)"/>���첽�汾
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance  / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="tenantId">Tenant's Id / �⻧ID</param>
        /// <param name="featureName">Unique feature name / ���ܵ�Ψһ����</param>
        /// <returns>Feature's current value / ���ܵĵ�ǰֵ</returns>
        public static string GetValue(this IFeatureChecker featureChecker, int tenantId, string featureName)
        {
            return AsyncHelper.RunSync(() => featureChecker.GetValueAsync(tenantId, featureName));
        }

        /// <summary>
        /// Checks if given feature is enabled.This should be used for boolean-value features.
        /// �������Ĺ����Ƿ���ã���ӦΪboolֵ�Ĺ���ʹ��
        /// This is a shortcut for <see cref="IsEnabledAsync(IFeatureChecker, int, string)"/> that uses <see cref="IAbpSession.TenantId"/> as tenantId.
        /// So, this method should be used only if TenantId can be obtained from the session.
        /// <see cref="GetValue(IFeatureChecker, int, string)"/>��<see cref="IAbpSession.TenantId"/>��Ϊ�⻧ID�Ŀ�ݷ�ʽ�����Դ˷���Ӧ�ñ�ʹ��Ϊ��Щ���Դ�session��ȡ�⻧IDֵ��
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="featureName">Unique feature name / ���ܵ�Ψһ����</param>
        /// <returns>True, if current feature's value is "true". / true,������ܵ�ֵ��true</returns>
        public static async Task<bool> IsEnabledAsync(this IFeatureChecker featureChecker, string featureName)
        {
            return string.Equals(await featureChecker.GetValueAsync(featureName), "true", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Checks if given feature is enabled.This should be used for boolean-value features.
        /// �������Ĺ����Ƿ���ã���ӦΪboolֵ�Ĺ���ʹ��
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="tenantId">Tenant's Id / �⻧ID</param>
        /// <param name="featureName">Unique feature name / ���ܵ�Ψһ����</param>
        /// <returns>True, if current feature's value is "true". / true,������ܵ�ֵ��true</returns>
        public static async Task<bool> IsEnabledAsync(this IFeatureChecker featureChecker, int tenantId, string featureName)
        {
            return string.Equals(await featureChecker.GetValueAsync(tenantId, featureName), "true", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Checks if given feature is enabled.This should be used for boolean-value features.
        /// �������Ĺ����Ƿ���ã���ӦΪboolֵ�Ĺ���ʹ��
        /// This is a shortcut for <see cref="IsEnabled(IFeatureChecker, int, string)"/> that uses <see cref="IAbpSession.TenantId"/> as tenantId.
        /// So, this method should be used only if TenantId can be obtained from the session.
        /// <see cref="GetValue(IFeatureChecker, int, string)"/>��<see cref="IAbpSession.TenantId"/>��Ϊ�⻧ID�Ŀ�ݷ�ʽ�����Դ˷���Ӧ�ñ�ʹ��Ϊ��Щ���Դ�session��ȡ�⻧IDֵ��
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="name">Unique feature name / ���ܵ�Ψһ����</param>
        /// <returns>True, if current feature's value is "true". / true,������ܵ�ֵ��true</returns>
        public static bool IsEnabled(this IFeatureChecker featureChecker, string name)
        {
            return AsyncHelper.RunSync(() => featureChecker.IsEnabledAsync(name));
        }

        /// <summary>
        /// Checks if given feature is enabled.This should be used for boolean-value features.
        /// �������Ĺ����Ƿ���ã���ӦΪboolֵ�Ĺ���ʹ��
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="tenantId">Tenant's Id / �⻧ID</param>
        /// <param name="featureName">Unique feature name / ���ܵ�Ψһ����</param>
        /// <returns>True, if current feature's value is "true". / true,������ܵ�ֵ��true</returns>
        public static bool IsEnabled(this IFeatureChecker featureChecker, int tenantId, string featureName)
        {
            return AsyncHelper.RunSync(() => featureChecker.IsEnabledAsync(tenantId, featureName));
        }

        /// <summary>
        /// Used to check if one of all given features are enabled.
        /// �������Ĺ����Ƿ����
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="requiresAll">True, to require all given features are enabled. False, to require one or more. / true,���и����Ĺ��ܶ�������ã�false��һ���������ü���</param>
        /// <param name="featureNames">Name of the features / ��������</param>
        public static async Task<bool> IsEnabledAsync(this IFeatureChecker featureChecker, bool requiresAll, params string[] featureNames)
        {
            if (featureNames.IsNullOrEmpty())
            {
                return true;
            }

            if (requiresAll)
            {
                foreach (var featureName in featureNames)
                {
                    if (!(await featureChecker.IsEnabledAsync(featureName)))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                foreach (var featureName in featureNames)
                {
                    if (await featureChecker.IsEnabledAsync(featureName))
                    {
                        return true;
                    }
                }

                return false;
            }
        }


        /// <summary>
        /// Used to check if one of all given features are enabled.
        /// �������Ĺ����Ƿ����
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="tenantId">Tenant id / �⻧ID</param>
        /// <param name="requiresAll">True, to require all given features are enabled. False, to require one or more. / true,���и����Ĺ��ܶ�������ã�false��һ���������ü���</param>
        /// <param name="featureNames">Name of the features / ��������</param>
        public static async Task<bool> IsEnabledAsync(this IFeatureChecker featureChecker, int tenantId, bool requiresAll, params string[] featureNames)
        {
            if (featureNames.IsNullOrEmpty())
            {
                return true;
            }

            if (requiresAll)
            {
                foreach (var featureName in featureNames)
                {
                    if (!(await featureChecker.IsEnabledAsync(tenantId, featureName)))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                foreach (var featureName in featureNames)
                {
                    if (await featureChecker.IsEnabledAsync(tenantId, featureName))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Used to check if one of all given features are enabled.
        /// �������Ĺ����Ƿ����
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="requiresAll">True, to require all given features are enabled. False, to require one or more. / true,���и����Ĺ��ܶ�������ã�false��һ���������ü���</param>
        /// <param name="featureNames">Name of the features / ��������</param>
        public static bool IsEnabled(this IFeatureChecker featureChecker, bool requiresAll, params string[] featureNames)
        {
            return AsyncHelper.RunSync(() => featureChecker.IsEnabledAsync(requiresAll, featureNames));
        }

        /// <summary>
        /// Used to check if one of all given features are enabled.
        /// �������Ĺ����Ƿ����
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="tenantId">Tenant id / �⻧ID</param>
        /// <param name="requiresAll">True, to require all given features are enabled. False, to require one or more. / true,���и����Ĺ��ܶ�������ã�false��һ���������ü���</param>
        /// <param name="featureNames">Name of the features / ��������</param>
        public static bool IsEnabled(this IFeatureChecker featureChecker, int tenantId, bool requiresAll, params string[] featureNames)
        {
            return AsyncHelper.RunSync(() => featureChecker.IsEnabledAsync(tenantId, requiresAll, featureNames));
        }

        /// <summary>
        /// Checks if given feature is enabled. Throws <see cref="AbpAuthorizationException"/> if not.
        /// �������Ĺ����Ƿ���ã�����������׳�<see cref="AbpAuthorizationException"/>
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="featureName">Unique feature name / ��������</param>
        public static async Task CheckEnabledAsync(this IFeatureChecker featureChecker, string featureName)
        {
            if (!(await featureChecker.IsEnabledAsync(featureName)))
            {
                throw new AbpAuthorizationException("Feature is not enabled: " + featureName);
            }
        }

        /// <summary>
        /// Checks if given feature is enabled. Throws <see cref="AbpAuthorizationException"/> if not.
        /// �������Ĺ����Ƿ���ã�����������׳�<see cref="AbpAuthorizationException"/>
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="featureName">Unique feature name / ��������</param>
        public static void CheckEnabled(this IFeatureChecker featureChecker, string featureName)
        {
            if (!featureChecker.IsEnabled(featureName))
            {
                throw new AbpAuthorizationException("Feature is not enabled: " + featureName);
            }
        }

        /// <summary>
        /// Checks if one of all given features are enabled. Throws <see cref="AbpAuthorizationException"/> if not.
        /// ���������й�����һ���Ƿ���ã�����������׳�<see cref="AbpAuthorizationException"/>
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="requiresAll">True, to require all given features are enabled. False, to require one or more. / true,���и����Ĺ��ܶ�������ã�false��һ���������ü���</param>
        /// <param name="featureNames">Name of the features / ��������</param>
        public static async Task CheckEnabledAsync(this IFeatureChecker featureChecker, bool requiresAll, params string[] featureNames)
        {
            if (featureNames.IsNullOrEmpty())
            {
                return;
            }

            if (requiresAll)
            {
                foreach (var featureName in featureNames)
                {
                    if (!(await featureChecker.IsEnabledAsync(featureName)))
                    {
                        throw new AbpAuthorizationException(
                            "Required features are not enabled. All of these features must be enabled: " +
                            string.Join(", ", featureNames)
                            );
                    }
                }
            }
            else
            {
                foreach (var featureName in featureNames)
                {
                    if (await featureChecker.IsEnabledAsync(featureName))
                    {
                        return;
                    }
                }

                throw new AbpAuthorizationException(
                    "Required features are not enabled. At least one of these features must be enabled: " +
                    string.Join(", ", featureNames)
                    );
            }
        }

        /// <summary>
        /// Checks if one of all given features are enabled. Throws <see cref="AbpAuthorizationException"/> if not.
        /// ���������й�����һ���Ƿ���ã�����������׳�<see cref="AbpAuthorizationException"/>
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="tenantId">Tenant id / �⻧ID</param>
        /// <param name="requiresAll">True, to require all given features are enabled. False, to require one or more. / true,���и����Ĺ��ܶ�������ã�false��һ���������ü���</param>
        /// <param name="featureNames">Name of the features / ��������</param>
        public static async Task CheckEnabledAsync(this IFeatureChecker featureChecker, int tenantId, bool requiresAll, params string[] featureNames)
        {
            if (featureNames.IsNullOrEmpty())
            {
                return;
            }

            if (requiresAll)
            {
                foreach (var featureName in featureNames)
                {
                    if (!(await featureChecker.IsEnabledAsync(tenantId, featureName)))
                    {
                        throw new AbpAuthorizationException(
                            "Required features are not enabled. All of these features must be enabled: " +
                            string.Join(", ", featureNames)
                            );
                    }
                }
            }
            else
            {
                foreach (var featureName in featureNames)
                {
                    if (await featureChecker.IsEnabledAsync(tenantId, featureName))
                    {
                        return;
                    }
                }

                throw new AbpAuthorizationException(
                    "Required features are not enabled. At least one of these features must be enabled: " +
                    string.Join(", ", featureNames)
                    );
            }
        }

        /// <summary>
        /// Checks if one of all given features are enabled. Throws <see cref="AbpAuthorizationException"/> if not.
        /// ���������й�����һ���Ƿ���ã�����������׳�<see cref="AbpAuthorizationException"/>
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="requiresAll">True, to require all given features are enabled. False, to require one or more. / true,���и����Ĺ��ܶ�������ã�false��һ���������ü���</param>
        /// <param name="featureNames">Name of the features / ��������</param>
        public static void CheckEnabled(this IFeatureChecker featureChecker, bool requiresAll, params string[] featureNames)
        {
            AsyncHelper.RunSync(() => featureChecker.CheckEnabledAsync(requiresAll, featureNames));
        }

        /// <summary>
        /// Checks if one of all given features are enabled. Throws <see cref="AbpAuthorizationException"/> if not.
        /// ���������й�����һ���Ƿ���ã�����������׳�<see cref="AbpAuthorizationException"/>
        /// </summary>
        /// <param name="featureChecker"><see cref="IFeatureChecker"/> instance / <see cref="IFeatureChecker"/>ʵ��</param>
        /// <param name="tenantId">Tenant id / �⻧ID</param>
        /// <param name="requiresAll">True, to require all given features are enabled. False, to require one or more. / true,���и����Ĺ��ܶ�������ã�false��һ���������ü���</param>
        /// <param name="featureNames">Name of the features / ��������</param>
        public static void CheckEnabled(this IFeatureChecker featureChecker, int tenantId, bool requiresAll, params string[] featureNames)
        {
            AsyncHelper.RunSync(() => featureChecker.CheckEnabledAsync(tenantId, requiresAll, featureNames));
        }
    }
}