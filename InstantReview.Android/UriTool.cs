using Android.Provider;
using Uri = Android.Net.Uri;

namespace InstantReview.Droid
{
    public class UriTool
    {
        public string GetActualPathFromFile(Uri uri)
        {
            string docId;
            using (var c1 = MainActivity.Instance.ContentResolver.Query(uri, null, null, null, null))
            {
                c1.MoveToFirst();
                var documentId = c1.GetString(0);
                docId = documentId.Substring(documentId.LastIndexOf(":") + 1);
            }

            string path;

            // The projection contains the columns we want to return in our query.
            const string selection = MediaStore.Images.Media.InterfaceConsts.Id + " =? ";
            using (var cursor = MainActivity.Instance.ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri, null, selection, new[] { docId }, null))
            {
                if (cursor == null) return null;
                var columnIndex = cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.Data);
                cursor.MoveToFirst();
                path = cursor.GetString(columnIndex);
            }
            return path;
        }
    }
}