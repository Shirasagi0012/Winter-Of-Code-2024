using SastImg.Client.Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastImg.Client.Model
{
    internal record ImageModel // 不建议直接使用ImageDto。ImageDto是用于传输数据的，
                               // 为了便于传输，其中使用ID代替能直接使用的字段，
                               // 因此，最好重新构建一个Model，将Dto中的内容转换为能直接在界面上展示的数据
    {
        public long Id { get; set; }

        public long UploaderId { get; set; }

        public long AlbumId { get; set; }

        public long[] TagIds = [];

        public string? Title { get; set; } = "";

        public string? Uploader { get; set; } = "";

        public string? Album { get; set; } = "";

        public string[] Tags = [];

        public DateTimeOffset? UploadedAt { get; set; }

        public DateTimeOffset? RemovedAt { get; set; }

        public static async Task<ImageModel> GetModelAsync(ImageDto dto, CancellationToken ct = default)
        {
            return new ImageModel()
            {
                Id = dto.Id,
                UploaderId = dto.UploaderId,
                AlbumId = dto.AlbumId,
                TagIds = [.. dto.Tags],

                Title = await GetTitleAsync(dto.Id, ct),
                Uploader = await GetUploaderAsync(dto.UploaderId, ct),
                Album = await GetAlbumAsync(dto.AlbumId, ct),
                Tags = await GetTagAsync([.. dto.Tags], ct),

                UploadedAt = dto.UploadedAt,
                RemovedAt = dto.RemovedAt,
            };

            async Task<string?> GetTitleAsync(long id, CancellationToken ct = default)
            {
                var title_r = await App.API.Image.GetDetailedImageAsync(id, ct);
                if (!title_r.IsSuccessful) return null;
                return title_r.Content.Title;
            }
            
            async Task<string?> GetUploaderAsync(long uploaderId, CancellationToken ct = default)
            {
                var uploader_r = await App.API.User.GetProfileInfoAsync(uploaderId, ct);
                if (!uploader_r.IsSuccessful) return null;
                return uploader_r.Content.Username;
            }

            async Task<string?> GetAlbumAsync(long albumId, CancellationToken ct = default)
            {
                var album_r = await App.API.Album.GetDetailedAlbumAsync(albumId, ct);
                if (!album_r.IsSuccessful) return null;
                return album_r.Content.Title;
            }

            async Task<string[]> GetTagAsync(long[] tagId, CancellationToken ct = default)
            {
                var id_r = await App.API.Tag.GetTagsAsync("", ct);
                if (!id_r.IsSuccessful) return [];
                return [.. id_r.Content.Where(x => tagId.Contains(x.Id)).Select(x => x.Name)];
            }
        }
    }
}
