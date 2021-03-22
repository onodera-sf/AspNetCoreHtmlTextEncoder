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

		// このメソッドはランタイムによって呼び出されます。 このメソッドを使用して、コンテナーにサービスを追加します。
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();

			// 全ての文字を実態参照で出力されないようにします。
			// 全てではなく特定の範囲のみをエンコードさせたくない場合は UnicodeRanges.All のプロパティを個別に設定します。
			services.Configure<WebEncoderOptions>(options =>
			{
				options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);

				// 個別に設定する場合 (例)
				//options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.Hiragana, UnicodeRanges.Katakana);
			});
		}

		// このメソッドはランタイムによって呼び出されます。 このメソッドを使用して、HTTP要求パイプラインを構成します。
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// デフォルトのHSTS値は30日です。 本番シナリオではこれを変更することをお勧めします。https://aka.ms/aspnetcore-hsts を参照してください。
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
