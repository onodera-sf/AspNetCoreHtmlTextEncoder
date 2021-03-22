using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace HtmlTextEncoder
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// ���̃��\�b�h�̓����^�C���ɂ���ČĂяo����܂��B ���̃��\�b�h���g�p���āA�R���e�i�[�ɃT�[�r�X��ǉ����܂��B
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();

			// �S�Ă̕��������ԎQ�Ƃŏo�͂���Ȃ��悤�ɂ��܂��B
			// �S�Ăł͂Ȃ�����͈݂̔͂̂��G���R�[�h���������Ȃ��ꍇ�� UnicodeRanges.All �̃v���p�e�B���ʂɐݒ肵�܂��B
			services.Configure<WebEncoderOptions>(options =>
			{
				options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);

				// �ʂɐݒ肷��ꍇ (��)
				//options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.Hiragana, UnicodeRanges.Katakana);
			});
		}

		// ���̃��\�b�h�̓����^�C���ɂ���ČĂяo����܂��B ���̃��\�b�h���g�p���āAHTTP�v���p�C�v���C�����\�����܂��B
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// �f�t�H���g��HSTS�l��30���ł��B �{�ԃV�i���I�ł͂����ύX���邱�Ƃ������߂��܂��Bhttps://aka.ms/aspnetcore-hsts ���Q�Ƃ��Ă��������B
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
