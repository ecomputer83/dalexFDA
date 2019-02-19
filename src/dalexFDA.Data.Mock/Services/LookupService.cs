﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dalexFDA.Abstractions;

namespace dalexFDA.Data.Mock
{
    public class LookupService : ILookupService
    {
        public LookupService()
        {
        }

        public Task<IEnumerable<Bank>> GetBanks()
        {
            var bankList = new List<Bank>
            {
                new Bank{ Name="Access Bank", Code=303 },
                new Bank{ Name="Zenith",Code=058 },
                new Bank{ Name="First Bank", Code=014 }
            };
            return Task.FromResult(bankList.OrderBy(x => x.Name).AsEnumerable());
        }

        public Task<string> GetPrivacyPolicy()
        {
            string retVal = @"This privacy notice discloses the privacy practices for (website address). This privacy notice applies solely to information collected by this website. It will notify you of the following: What personally identifiable information is collected from you through the website, how it is used and with whom it may be shared.
                            What choices are available to you regarding the use of your data.
                            The security procedures in place to protect the misuse of your information.
                            How you can correct any inaccuracies in the information.
                            Information Collection, Use, and Sharing
                            We are the sole owners of the information collected on this site.We only have access to / collect information that you voluntarily give us via email or other direct contact from you.We will not sell or rent this information to anyone.

                            We will use your information to respond to you, regarding the reason you contacted us. We will not share your information with any third party outside of our organization, other than as necessary to fulfill your request, e.g.to ship an order.

                            Unless you ask us not to, we may contact you via email in the future to tell you about specials, new products or services, or changes to this privacy policy.

                            Your Access to and Control Over Information
                            You may opt out of any future contacts from us at any time.You can do the following at any time by contacting us via the email address or phone number given on our website:

                            See what data we have about you, if any.
                            Change / correct any data we have about you.
                            Have us delete any data we have about you.
                            Express any concern you have about our use of your data.
                            Security
                            We take precautions to protect your information. When you submit sensitive information via the website, your information is protected both online and offline.

                            Wherever we collect sensitive information (such as credit card data), that information is encrypted and transmitted to us in a secure way.You can verify this by looking for a lock icon in the address bar and looking for 'https' at the beginning of the address of the Web page.


                            While we use encryption to protect sensitive information transmitted online, we also protect your information offline. Only employees who need the information to perform a specific job (for example, billing or customer service) are granted access to personally identifiable information. The computers/servers in which we store personally identifiable information are kept in a secure environment.

                            If you feel that we are not abiding by this privacy policy, you should contact us immediately via telephone at XXX YYY-ZZZZ or via email.";
            return Task.FromResult(retVal);
        }

        public Task<string> GetTermsAndConditions()
        {
            string retVal = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam viverra porta risus vel hendrerit. Phasellus vitae condimentum lorem. Sed a urna tortor. Donec rhoncus tempor leo, ut ullamcorper mauris. Donec vitae cursus nisi. Pellentesque non nibh quis odio viverra ultrices. Nulla vel ante et ante fermentum porttitor et non est. Nam tincidunt ipsum elit, at pharetra tellus imperdiet a. Duis id orci efficitur, gravida quam vitae, maximus est. Donec laoreet, libero ac volutpat consequat, nulla magna ornare diam, quis placerat magna nunc at orci. Etiam mattis velit ut neque eleifend pharetra. Praesent nec est nunc. In non sem risus. Fusce semper volutpat vulputate. Praesent elementum suscipit laoreet.
                                Curabitur egestas ultrices interdum. Nulla lobortis tincidunt magna in tincidunt. Vivamus eget eros porttitor, venenatis risus at, posuere arcu. Vivamus mattis nulla sed nulla elementum semper. Fusce vulputate ante sit amet vulputate convallis. Suspendisse imperdiet scelerisque sem feugiat placerat. Aenean dignissim neque velit, a varius risus fringilla nec. Donec rhoncus augue ut tincidunt tincidunt.";
            return Task.FromResult(retVal);
        }
    }
}
