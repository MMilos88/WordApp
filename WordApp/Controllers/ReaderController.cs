using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WordApp.BusinessInterface;
using WordApp.BusinessModel;
using WordApp.BusinessModel.Requests;
using WordApp.BusinessModel.Responses;
using System.IO;

namespace WordApp.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Allow_Any")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        #region Private fields
        private readonly IReaderBusinessLogic _readerBusinessLogic;
        #endregion

        #region Public constructors
        public ReaderController(IReaderBusinessLogic readerBusinessLogic)
        {
            _readerBusinessLogic = readerBusinessLogic;           
        
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Count words from input text
        /// </summary>
        /// <param name="request">CalculateWordCountRequest</param>
        /// <returns></returns>
        [HttpPost(nameof(CalculateWordCount))]
        [ProducesResponseType(200, Type = typeof(Result<CalculateWordCountResponse>))]
        public async Task<IActionResult> CalculateWordCount([FromBody]CalculateWordCountRequest request)
        {
              return Ok(await _readerBusinessLogic.CalculateWordCount(request));
        }

        /// <summary>
        /// Count words database.
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(CalculateWordCountFromDB))]
        [ProducesResponseType(200, Type = typeof(Result<CalculateWordCountResponse>))]
        public async Task<IActionResult> CalculateWordCountFromDB()
        {
            return Ok(await _readerBusinessLogic.CalculateWordCountFromDB());
        }

        /// <summary>
        /// Count words from input file
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(CalculateWordCountFromFile))]
        [ProducesResponseType(200, Type = typeof(Result<CalculateWordCountResponse>))]
        public async Task<IActionResult> CalculateWordCountFromFile()
        {
            
            var file = Request.Form.Files.Count > 0 ?
                   Request.Form.Files[0] : null;
          
                Stream stream = file.OpenReadStream();
                return Ok(await _readerBusinessLogic.CalculateWordCountFromFile(stream));
        }
        #endregion
    }
}

