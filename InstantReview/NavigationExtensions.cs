using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InstantReview
{
    public static class NavigationExtensions
    {
        public static async Task<Page> PopAndDisposeModalAsync<T>(this INavigation navigation)
        {
            Page page = null;

            // Check that the page being popped is of the correct type to prevent popping the same page twice, e.g. if user quickly double taps a button
            if (navigation.ModalStack.LastOrDefault() is T)
            {
                page = await navigation.PopModalAsync();
                (page as IDisposable)?.Dispose();
            }

            return page;
        }

        public static void RemovePage<T>(this INavigation navigation) where T : Page
        {
            var page = navigation.NavigationStack.OfType<T>().FirstOrDefault();
            if (page != null)
            {
                navigation.RemovePage(page);
            }
        }

        // Extension methods for preventing double taps opening the same page multiple times
        public static async Task PushModalAsyncSingle(this INavigation navigation, Page page)
        {
            if (navigation.ModalStack.Count == 0 ||
                navigation.ModalStack.Last().GetType() != page.GetType())
            {
                await navigation.PushModalAsync(page);
            }
        }

        public static async Task PushAsyncSingle(this INavigation navigation, Page page)
        {
            if (navigation.NavigationStack.Count == 0 ||
                navigation.NavigationStack.Last().GetType() != page.GetType())
            {
                await navigation.PushAsync(page);
            }
        }

        public static async Task PopToRootAndDispose(this INavigation navigation)
        {
            for (var i = 1; i < navigation.NavigationStack.Count; ++i)
            {
                var page = navigation.NavigationStack[i];
                (page as IDisposable)?.Dispose();
            }
            await navigation.PopToRootAsync();
        }
    }
}